using UnityEngine;
using UnityEngine.InputSystem;

namespace combat_system
{
    public class DraggingController : MonoBehaviour
    {
        public InputActionAsset inputActions;
        private bool isDragging = false;
        private Vector2 dragStartPosition;
        [SerializeField] private Camera mainCamera;
        private InputAction clickAction;
        private InputAction pointAction;
        private Vector3 offset;
        private GameObject draggedObject;
        void Awake()
        {
            clickAction = inputActions.FindActionMap("UI").FindAction("Click");
            pointAction = inputActions.FindActionMap("UI").FindAction("Point");
        }
        void OnEnable()
        {
            clickAction.Enable();
            pointAction.Enable();
            clickAction.started += OnDragStart;
            clickAction.canceled += OnDragEnd;
        }

        private void OnDragEnd(InputAction.CallbackContext context)
        {
            isDragging = false;
            if (draggedObject != null)
            {
                Debug.Log("back to origin");
                draggedObject = null;
            }
        }

        private void OnDragStart(InputAction.CallbackContext context)
        {
            isDragging = true;
            Debug.Log("drag start");
            dragStartPosition = pointAction.ReadValue<Vector2>();
            
            Ray ray = mainCamera.ScreenPointToRay(dragStartPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.CompareTag("Draggable"))
                {
                    draggedObject = hit.collider.gameObject;
                    Vector3 worldPos = hit.point;
                    offset = draggedObject.transform.position - worldPos;
                }
            }
        }

        void OnDisable()
        {
            clickAction.started -= OnDragStart;
            clickAction.canceled -= OnDragEnd;
            clickAction.Disable();
            pointAction.Disable();
        }

        void Update()
        {
            if (isDragging && draggedObject)
            {
                Vector2 currentPosition = pointAction.ReadValue<Vector2>();
                Ray ray = mainCamera.ScreenPointToRay(currentPosition);
                
                Vector3 worldPos = ray.GetPoint(10f);
                draggedObject.transform.position = worldPos + offset;
            }
        }
    }
}