using System;
using System.Collections.Generic;
using card_system.data;
using card_system.functionality;
using model.entity;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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

        public void OnEndDrag(PointerEventData eventData)
        {
            //for now only manualTargeting returns a target , the other cards just return
            if (manualTargeting)
            {
                //this here target must be found i guess
                TargetingController.instance.HideArrow();
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