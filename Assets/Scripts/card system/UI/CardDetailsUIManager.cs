using System;
using System.Collections.Generic;
using card_system.functionality;
using global_events;
using persistence_system.model;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
//TODO: Need to fix the shit with lagging window
//issues with lagging window , specifically with the window coveing the card ,
// triggering the hover end of the card , and then after siapperaing triggering the hovering again creating a cycle
namespace card_system.UI
{
    public class CardDetailsUIManager : MonoBehaviour
    {
        [SerializeField]private GameObject detailsWindow;
        [SerializeField]private GameObject detailedEffectPrefab;
        private List<GameObject> activeEffects = new();
        private Queue<GameObject> pool = new();
        
        private int usedSlots;

        public TextMeshProUGUI usedSlotsUI;

        private void Awake()
        {
            var cg = detailsWindow.GetComponent<CanvasGroup>();
            if (cg == null) cg = detailsWindow.AddComponent<CanvasGroup>();
            cg.blocksRaycasts = false;
            cg.interactable = false;
        }
        private void ReturnAllToPool()
        {
            foreach (var effect in activeEffects)
            {
                if (effect != null)
                {
                    effect.SetActive(false);
                    pool.Enqueue(effect);
                }
            }
            activeEffects.Clear();
        }
        
        private GameObject GetFromPool()
        {
            GameObject effect;
            if (pool.Count > 0)
            {
                effect = pool.Dequeue();
                effect.SetActive(true);
            }
            else
            {
                effect = Instantiate(detailedEffectPrefab, detailsWindow.transform);
            }
            activeEffects.Add(effect);
            return effect;
        }
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
            ReturnAllToPool();
        }

        private void HandleMouseHover(List<CardEffect> cardEffects)
        {
            usedSlots = 0;
            ReturnAllToPool();
            detailsWindow.SetActive(true);
            //Need to List them off with maybe the prefab already created(would need to resize that motherfucker)
            foreach (var effect in cardEffects)
            {
                GameObject detailedEffect = GetFromPool();
                usedSlots += effect.slotCost;
                var description = detailedEffect.GetComponentInChildren<TextMeshProUGUI>();
                if (description != null)
                {
                    description.SetText(effect.BuildDescription());
                    description.color = Color.white;
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
            ShowUsedSlots();
        }
        public void ShowUsedSlots()
        {
            usedSlotsUI.text = usedSlots.ToString() + "/6";
        }
    }
}