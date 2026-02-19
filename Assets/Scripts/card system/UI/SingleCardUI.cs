using System;
using System.Collections.Generic;
using card_system.data;
using card_system.functionality;
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
        [SerializeField]private bool manualTargeting;
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
            cardName.text = data.name;
            image.sprite = data.image;
            manualTargeting = data.manualTargeting;
            cardEffects = data.cardEffects;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            originalPosition = rectTransform.position;
            originalParent = transform.parent;
            originalSiblingIndex = transform.GetSiblingIndex();
            if (manualTargeting)
            {
                canvasGroup.alpha = 0;
                TargetingController.instance.SetUpArrow(transform.position,this);
                
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
            if (manualTargeting)
            {
                Vector2 screenMousePos = Mouse.current.position.ReadValue();
                Vector2 worldMousePos =  Camera.main.ScreenToWorldPoint(screenMousePos);
                Ray ray =  Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit2D hit = Physics2D.Raycast(worldMousePos, Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log($"HIT 2D: {hit.transform.name}");
                    Enemy enemyHit = hit.transform.GetComponentInChildren<Enemy>();
                    if (enemyHit)
                    {
                        GlobalEvents.RaiseTargetSelected(enemyHit);
                        Debug.Log($"Enemy selected: {enemyHit.name}");
                    }
                    else
                    {
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
                ReturnCard();
            }
        }

        public void PlayCard(Entity target)
        {
            foreach (CardEffect effect in cardEffects)
            {
                effect.Execute(target);
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