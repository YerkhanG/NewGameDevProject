using System;
using System.Collections.Generic;
using System.Linq;
using card_system.animation;
using card_system.data;
using card_system.functionality;
using combat_system;
using global_events;
using model.entity;
using persistence_system.helpers;
using persistence_system.model;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace card_system.UI
{
    public class SingleCardController : MonoBehaviour, IBeginDragHandler ,IEndDragHandler, IDragHandler
    {
        public CardData cardData;
        //for new saving
        public CardInstanceRecord instanceRecord; 
        [SerializeField]private TextMeshProUGUI cardName;
        [SerializeField]private TextMeshProUGUI description;
        [SerializeField]private TextMeshProUGUI manaCost;
        [SerializeField]private Image image;
        public List<CardEffect> cardEffects;
        public bool isManual;
        [SerializeField] private Canvas canvas;
        [SerializeField]private CanvasGroup canvasGroup;
        [SerializeField]private RectTransform rectTransform;
        //Original position for snapping back
        private Vector2 originalPosition;
        private Transform originalParent;
        private int originalSiblingIndex;
        
        public CardData GetCardData => cardData;
        public CardInstanceRecord GetCardInstanceRecord => instanceRecord;
        void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
        }
        public void Setup(CardInstanceRecord record)
        {
            instanceRecord = record;
            CardData data = CardRegistry.instance.GetCard(record.templateId);
            cardData = data;
            manaCost.text = data.manaCost;
            cardName.text = data.cardName;
            image.sprite = data.image;
            cardEffects = CardEffectBuilder.BuildRuntimeEffects(record);
            isManual = cardEffects.Any(e => e.targetType == TargetType.ManualTargeting);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            originalPosition = rectTransform.position;
            originalParent = transform.parent;
            originalSiblingIndex = transform.GetSiblingIndex();
            if (isManual)
            {
                canvasGroup.alpha = 0;
                TargetingManager.instance.SetUpArrow(transform.position,this);
            }   
            else
            {
                transform.SetParent(canvas.transform, true);
                transform.SetAsLastSibling();
                canvasGroup.alpha = 0.6f;
                canvasGroup.blocksRaycasts = false;   
            }
        }
        // here the new targeting will decide to activate card or not 
        public void OnEndDrag(PointerEventData eventData)
        {
            if (isManual)
            {
                Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                RaycastHit2D hit = Physics2D.Raycast(worldMousePos, Vector2.zero);
                if (hit.collider != null)
                {
                    Enemy enemyHit = hit.transform.GetComponentInChildren<Enemy>();
                    if (enemyHit)
                    {
                        GlobalEvents.RaiseTargetSelected(enemyHit);
                        TargetingManager.instance.HideArrow();
                        return;   // important: do NOT call ReturnCard here
                    }
                }
                TargetingManager.instance.HideArrow();
                ReturnCard();
            }
            else
            {
                bool played = PlayCard();
                if (!played)
                {
                    ReturnCard();
                }
                else
                {
                    Debug.Log("[Card] PlayCard returned true – card successfully played.");
                }
            }
        }

        public bool PlayCard(Entity target = null)
        {
            if (!ManaCountManager.instance.TryToSpendMana(int.Parse(manaCost.text)))
                return false;

            // 1. Immediately detach from hand and disable interaction
            transform.SetParent(null);
            canvasGroup.blocksRaycasts = false;

            // 2. Add data to graveyard ONCE
            GraveyardPileManager.instance.graveyardPile.Add(instanceRecord);
            // 3. Execute effects
            EffectContext context = new EffectContext
            {
                caster = (Player)CombatEntityManager.instance.mainCharacter,
                manualTargetEntity = target,
                allTargets = CombatEntityManager.instance.GetAllEnemies(),
                isManual = isManual
            };
            foreach (CardEffect effect in cardEffects)
            {
                effect.Execute(context);
                Debug.Log("Effect played " + effect.name);
            }
                

            // 4. Play animation and destroy the GameObject
            var anim = GetComponent<CardAnimationController>();
            Vector3 targetPos = target == null ? transform.position : target.transform.position;
            anim.AnimatePlay(targetPos, () =>
            {
                Destroy(gameObject);
            });

            return true;
        }
        private void ReturnCard()
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            transform.SetParent(originalParent, true);
            transform.SetSiblingIndex(originalSiblingIndex);
            rectTransform.position = originalPosition; 
        }

        public void OnDrag(PointerEventData eventData)
        {
            // Direct screen to local conversion
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.GetComponent<RectTransform>(),
                eventData.position,
                canvas.worldCamera,
                out localPoint
            );
    
            rectTransform.position = canvas.transform.TransformPoint(localPoint);
        }
    }
}