using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace logging
{
    public class ClickLogger : MonoBehaviour
    {
        
        void Start()
        {
            Debug.Log($"EventSystem exists: {EventSystem.current != null}");
            if (EventSystem.current != null)
            {
                Debug.Log($"EventSystem active: {EventSystem.current.gameObject.activeInHierarchy}");
                Debug.Log($"Current input module: {EventSystem.current.currentInputModule?.GetType().Name}");
            }

            // Find your Canvas (replace with the actual name)
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas == null) canvas = FindObjectOfType<Canvas>();
            if (canvas != null)
            {
                GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();
                Debug.Log($"Canvas found: {canvas.name}, active: {canvas.gameObject.activeInHierarchy}");
                Debug.Log($"GraphicRaycaster present: {raycaster != null}, enabled: {raycaster?.enabled}");
            }
        }
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            Debug.Log("=== CLICK DETECTED ===");
        
            // check what UI elements are under the pointer
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
        
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
        
            if (results.Count == 0)
            {
                Debug.Log("Nothing hit by UI raycast");
            }
            else
            {
                foreach (RaycastResult result in results)
                {
                    Debug.Log($"UI Hit: {result.gameObject.name} | depth: {result.depth} | distance: {result.distance}");
                }
            }
        }
    }
}