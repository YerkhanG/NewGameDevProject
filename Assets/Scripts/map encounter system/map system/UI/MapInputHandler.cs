using global_events;
using map_encounter_system.map_system.data.node;
using map_encounter_system.map_system.manager;
using map_encounter_system.map_system.UI.scrolling;
using UnityEngine;
using UnityEngine.InputSystem;

namespace map_encounter_system.map_system.UI
{
    public class MapInputHandler : MonoBehaviour
    {
        private PlayerControlls actions;

        private void Awake()
        {
            actions = new PlayerControlls();
            actions.player.Click.performed += OnClicked;
            actions.Enable();
        }

        private void OnClicked(InputAction.CallbackContext context)
        {
            /*if (ScrollNonUI.IsDragging) return;*/

            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);
    
            if (hit == null) return;

            NodeView node = hit.GetComponent<NodeView>();
            if (node == null) return;

            GlobalEvents.RaiseEncounterPicked(node.Node.type);
        }
        private void OnDestroy() => actions?.Dispose();
    }
}