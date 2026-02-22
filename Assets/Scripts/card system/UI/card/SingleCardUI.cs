using System;
using System.Collections.Generic;
using card_system.data;
using card_system.functionality;
using combat_system;
using global_events;
using model.entity;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace card_system.UI
{
    public class SingleCardUI : MonoBehaviour, IBeginDragHandler ,IEndDragHandler, IDragHandler 
    {
        /*[SerializeField]private CardData cardData;*/
        [SerializeField]private TextMeshProUGUI cardName;
        [SerializeField]private TextMeshProUGUI description;
        [SerializeField]private TextMeshProUGUI manaCost;
        [SerializeField]private Image image;
        public List<CardEffect> cardEffects; 
        [SerializeField] private Canvas canvas;
        [SerializeField]private CanvasGroup canvasGroup;
        [SerializeField]private TargetType targetType;
        [SerializeField]private RectTransform rectTransform;
        //Original position for snapping back
        private Vector2 originalPosition;
        private Transform originalParent;
        private int originalSiblingIndex;
        void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
        }
        public void Setup(CardData data)
        {
            Debug.Log("Setup 37: " + data.manaCost);
            manaCost.text = data.manaCost;
            cardName.text = data.name;
            image.sprite = data.image;
            targetType = data.targetType;
            cardEffects = data.cardEffects;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            originalPosition = rectTransform.position;
            originalParent = transform.parent;
            originalSiblingIndex = transform.GetSiblingIndex();
            if (targetType.Equals(TargetType.ManualTargeting))
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
            if (targetType.Equals(TargetType.ManualTargeting))
            {
                Vector2 screenMousePos = Mouse.current.position.ReadValue();
                Vector2 worldMousePos =  Camera.main.ScreenToWorldPoint(screenMousePos);
                Ray ray =  Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit2D hit = Physics2D.Raycast(worldMousePos, Vector2.zero);
                if (hit.collider != null)
                {
                    //TODO: after i finish the turn system
                    //for now its only about enemies.
                    //Soon i will need to make a generalaized version for everyone
                    //(you(heal , buff) or enemies(debuff or deal damage)
                    Debug.Log($"HIT 2D: {hit.transform.name}");
                    Enemy enemyHit = hit.transform.GetComponentInChildren<Enemy>();
                    if (enemyHit)
                    {
                        GlobalEvents.RaiseTargetSelected(enemyHit);
                        Debug.Log($"Enemy selected: {enemyHit.name}");
                    }
                    else
                    {
                        TargetingManager.instance.HideArrow();
                        Debug.Log("No Proper Target Selected" + hit.transform.name);
                    }
                }
                else
                {
                    Debug.Log("No Target Selected");
                }
                //For now it returns, later i will destroy the card after its played 
                ReturnCard();
            }
            else
            {
                //TODO: after i finish the turn system
                //Here i need to either play or not play a non manual targeting card 
                /*if(TargetingController.instance.)*/
                //first i need to finish the turn system
                ReturnCard();
            }
        }

        public void PlayCard(Entity target)
        {
            if (ManaCountManager.instance.TryToSpendMana(int.Parse(manaCost.text)))
            {
                Debug.Log("Mana Spent: " + manaCost.text);
                foreach (CardEffect effect in cardEffects)
                {
                    effect.Execute(target);
                }
            }
            else
            {
                Debug.Log("Mana Spend Failed");
            }
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