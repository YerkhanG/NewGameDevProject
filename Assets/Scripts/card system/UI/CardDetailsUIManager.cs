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
        private List<GameObject> activeEffects = new();
        private Queue<GameObject> pool = new();

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
            ReturnAllToPool();
            detailsWindow.SetActive(true);
            //Need to List them off with maybe the prefab already created(would need to resize that motherfucker)
            foreach (var effect in cardEffects)
            {
                GameObject detailedEffect = GetFromPool();
                var description = detailedEffect.GetComponentInChildren<TextMeshProUGUI>();
                if (description != null)
                {
                    description.SetText(effect.Description);
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
        }
    }
}