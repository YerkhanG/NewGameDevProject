using System;
using System.Collections;
using global_events;
using map_encounter_system.map_system.data;
using map_encounter_system.map_system.data.node;
using map_encounter_system.map_system.manager;
using persistence_system.manager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace map_encounter_system.map_system.UI
{
    public class MapInputHandler : MonoBehaviour
    {
        private PlayerControlls actions;

        public bool lockAfterSelect = false;
        public float enterNodeDelay = 1f;
        public bool Locked { get; set; }
        public Button BackButton;
        private void Awake()
        {
            actions = new PlayerControlls();
            actions.player.Click.performed += OnClicked;
            actions.Enable();
        }

        public void OnEnable()
        {
            BackButton.onClick.AddListener(OnBackButtonClicked);
        }

        public void OnDisable()
        {
            BackButton.onClick.RemoveListener(OnBackButtonClicked);
        }

        public void OnBackButtonClicked()
        {
            SceneManager.LoadScene("MainLobby");
        }
        private void OnClicked(InputAction.CallbackContext context)
        {
            StartCoroutine(HandleClick());
        }

        private IEnumerator HandleClick()
        {
            yield return null; // wait one frame

            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                yield break;

            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);
            if (!hit) yield break;

            NodeView nodeView = hit.GetComponent<NodeView>();
            if (!nodeView) yield break;

            Map map = MapManager.instance.map;

            if (map.path.Count == 0)
            {
                if (nodeView.Node.gridPosition.y != 0) yield break;
            }
            else
            {
                Node current = map.GetNode(map.path[^1]);
                if (!current.connections2.Contains(nodeView.Node.gridPosition)) yield break;
            }

            map.path.Add(nodeView.Node.gridPosition);
            PersistenceManager.instance.SaveSceneData(map);
            MapManager.instance.mapView.UpdateNodeStates(map);
            GlobalEvents.RaiseEncounterPicked(nodeView.Node.type);
        }
        private void OnDestroy() => actions?.Dispose();
    }
}