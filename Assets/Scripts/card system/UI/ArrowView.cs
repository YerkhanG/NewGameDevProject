using global_events;
using model.entity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace card_system.UI
{
    public class ArrowView : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform arrowHead;
        
        private Vector3 startPosition;
        private bool isActive = false;

        public void Show(Vector3 start)
        {
            isActive = true;
            startPosition = start;
            lineRenderer.enabled = true;
            arrowHead.gameObject.SetActive(true);
        }
    
        public void Hide()
        {
            isActive = false;
            lineRenderer.enabled = false;
            arrowHead.gameObject.SetActive(false);
        }
    
        void Update()
        {
            if (!isActive) return;
            
            Vector2 screenMousePos = Mouse.current.position.ReadValue();
            
            // Convert to world space
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(screenMousePos);
            mouseWorldPos.z = 0;
        
            // Update line points
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, mouseWorldPos);
        
            // Rotate arrowhead to face mouse
            Vector3 direction = mouseWorldPos - startPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrowHead.rotation = Quaternion.Euler(0, 0, angle - 90f);
            arrowHead.position = mouseWorldPos;
            
            //Choose target through the ray of the arrow
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
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
            }
            
        }
    }
}