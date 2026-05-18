using encounter_system.data;
using global_events;
using map_encounter_system.map_system.data.node;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace map_encounter_system.map_system.UI
{
    public class NodeView : MonoBehaviour
    {
        public SpriteRenderer nodeIconImage; // Assign this in your Inspector
        public Button nodeButton;
        public Node Node;

        public void Initialize(Node node)
        {
            this.Node = node;
            //Should Wire the Onclick
        }

        private void OnClicked()
        {
            GlobalEvents.RaiseEncounterPicked(Node.type);
        }

        public void SetInteractable(bool isClickable)
        {
            nodeButton.interactable = isClickable;
            
            Color c =  nodeIconImage.color;
            c.a = isClickable ? 1f : 0.3f;
            nodeIconImage.color = c;
        }
    }
}