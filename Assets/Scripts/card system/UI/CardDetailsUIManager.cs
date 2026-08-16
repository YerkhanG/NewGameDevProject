using System;
using System.Collections.Generic;
using card_system.functionality;
using global_events;
using persistence_system.model;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace card_system.UI
{
    public class CardDetailsUIManager : MonoBehaviour
    {
        [SerializeField]private GameObject detailsWindow;
        [SerializeField]private GameObject detailedEffectPrefab;

        public void OnEnable()
        {
            GlobalEvents.onMouseCardHoverStart += HandleMouseHover;
            GlobalEvents.onMouseCardHoverEnd += HandleMouseOuthover;
        }
        
        public void OnDisable()
        {
            GlobalEvents.onMouseCardHoverStart -= HandleMouseHover;
            GlobalEvents.onMouseCardHoverEnd -= HandleMouseOuthover;
        }

        private void HandleMouseOuthover(List<CardEffect> obj)
        {
            detailsWindow.SetActive(false);
            foreach (Transform child in detailsWindow.transform)
            {
                if (child.GetComponent<SingleUIEffectController>() != null)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        private void HandleMouseHover(List<CardEffect> cardEffects)
        {
            detailsWindow.SetActive(true);
            //Need to List them off with maybe the prefab already created(would need to resize that motherfucker)
            foreach (var effect in cardEffects)
            {
                GameObject detailedEffect = Instantiate(detailedEffectPrefab, detailsWindow.transform);
                var description = detailedEffect.GetComponentInChildren<TextMeshProUGUI>();
                if (description != null)
                {
                    Debug.Log("Checking text mesh field: " +  description);
                    Debug.Log("Checking effect desc: " +  effect.Description);
                    description.SetText(effect.Description);
                    //resize for the window 
                    LayoutElement  layoutElement = detailedEffect.GetComponent<LayoutElement>();
                    if (layoutElement != null)
                    {
                        layoutElement.minHeight = 150f;
                        layoutElement.minWidth = 300f;
                        layoutElement.preferredHeight = 150f;
                        layoutElement.preferredWidth = 300f;
                    }
                }
                else
                {
                    Debug.Log("Something wrong: " + description);
                    Debug.Log(description);
                }
            }
        }
    }
}