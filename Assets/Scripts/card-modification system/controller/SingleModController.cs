using System;
using card_modification_system.data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace card_modification_system.controller
{
    public class SingleModController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI modDescription;
        /*[SerializeField] private TextMeshProUGUI modName;*/
        [SerializeField] private Image background;

        private ModDefinition modData;
        private Action<ModDefinition> onSelected;

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
    }
}