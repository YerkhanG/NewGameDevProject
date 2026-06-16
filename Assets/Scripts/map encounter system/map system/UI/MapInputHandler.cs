using global_events;
using map_encounter_system.map_system.data;
using map_encounter_system.map_system.data.node;
using map_encounter_system.map_system.manager;
using map_encounter_system.map_system.UI.scrolling;
using persistence_system.manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace map_encounter_system.map_system.UI
{
    public class MapInputHandler : MonoBehaviour
    {
        private PlayerControlls actions;

        public bool lockAfterSelect = false;
        public float enterNodeDelay = 1f;
        public bool Locked { get; set; }

        private void Awake()
        {
            actions = new PlayerControlls();
            actions.player.Click.performed += OnClicked;
            actions.Enable();
        }

        private void OnClicked(InputAction.CallbackContext context)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);
            if (hit == null) return;

            NodeView nodeView = hit.GetComponent<NodeView>();
            if (nodeView == null) return;

            Map map = MapManager.instance.map;

            if (map.path.Count == 0)
            {
                if (nodeView.Node.gridPosition.y != 0) return;
            }
            else
            {
                Node current = map.GetNode(map.path[^1]);
                if (!current.connections2.Contains(nodeView.Node.gridPosition)) return;
            }

            map.path.Add(nodeView.Node.gridPosition);
            PersistenceManager.instance.SaveSceneData(map);
            MapManager.instance.mapView.UpdateNodeStates(map);
            GlobalEvents.RaiseEncounterPicked(nodeView.Node.type);
        }

        private void OnDestroy() => actions?.Dispose();
    }
}