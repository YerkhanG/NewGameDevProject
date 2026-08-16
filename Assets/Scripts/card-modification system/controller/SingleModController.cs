using System;
using card_modification_system.data;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace card_modification_system.controller
{
    public class SingleModController : MonoBehaviour, IPointerClickHandler , IPointerExitHandler,  IPointerEnterHandler
    {
        [SerializeField] private TextMeshProUGUI modDescription;
        /*[SerializeField] private TextMeshProUGUI modName;*/
        [SerializeField] private Image background;

        private ModDefinition modData;
        private Action<ModDefinition> onSelected;
        
        private void Start()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
        }
        
        public void SetUp(ModDefinition data , Action<ModDefinition> OnSelectedCallBack)
        {
            modData = data;
            modDescription.text = data.GetDescription();
            onSelected = OnSelectedCallBack;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            onSelected?.Invoke(modData);
        }

        // Optional: ensure the background Image blocks raycasts
        private void Awake()
        {
            if (background != null && !background.raycastTarget)
            {
                background.raycastTarget = true; // make sure it's clickable
            }
        }
        
        public void OnPointerEnter(PointerEventData eventData) 
        {
            background.color = new Color(0.15f, 0.15f, 0.15f); // lighter on hover
            transform.DOScale(1.02f, 0.1f); // subtle pop (requires DOTween)
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            background.color = Color.black; // back to normal
            transform.DOScale(1f, 0.1f);
        }
    }
}