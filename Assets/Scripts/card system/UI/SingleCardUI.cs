using System;
using card_system.data;
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
        [SerializeField] private Canvas canvas;
        [SerializeField]private CanvasGroup canvasGroup;
        private Vector2 originalPosition;
        [SerializeField]private RectTransform rectTransform;

        void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
        }
        public void Setup(CardData data)
        {
            cardName.text = data.name;
            image.sprite = data.image;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            originalPosition = rectTransform.anchoredPosition;
            transform.SetParent(canvas.transform, true);
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            var layoutElement = GetComponent<LayoutElement>();
            if (layoutElement != null)
                layoutElement.ignoreLayout = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            var layoutElement = GetComponent<LayoutElement>();
            if (layoutElement != null)
                layoutElement.ignoreLayout = false;
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