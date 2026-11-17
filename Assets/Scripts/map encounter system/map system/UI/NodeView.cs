using System;
using encounter_system.data;
using global_events;
using map_encounter_system.map_system.data.node;
using map_encounter_system.map_system.manager;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace map_encounter_system.map_system.UI
{
    public enum NodeStates
    {
        Locked , 
        Visited,
        Attainable
    }
    public class NodeView : MonoBehaviour
    {
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite hardSprite;
        [SerializeField] private Sprite eliteSprite;
        [SerializeField] private Sprite bossSprite;
        [SerializeField] private Sprite upgradeSprite;
        [SerializeField] private Sprite cardPickSprite;
        public SpriteRenderer nodeIconImage; // Assign this in your Inspector
        public Node Node;

        public void Initialize(Node node)
        {
            Node = node;
            if (node.nodeCategory == NodeCategory.Event)
            {
                nodeIconImage.sprite = node.eventType switch
                {
                    NodeEventType.Upgrade  => upgradeSprite,
                    NodeEventType.CardPick => cardPickSprite,
                    _ => normalSprite
                };
                return;
            }
            nodeIconImage.sprite = node.type switch
            {
                Encounter.Rarity.Normal => normalSprite,
                Encounter.Rarity.Hard => hardSprite,
                Encounter.Rarity.Elite => eliteSprite,
                Encounter.Rarity.Boss => bossSprite,
                _ => normalSprite
            };
        }

        public void SetInteractable(bool isClickable)
        {

            Color c = nodeIconImage.color;
            c.a = isClickable ? 1f : 0.3f;
            nodeIconImage.color = c;
        }
        
        public void SetState(NodeStates state)
        {
            switch (state)
            {
                case NodeStates.Locked:
                    SetInteractable(false);
                    break;
                case NodeStates.Visited:
                    SetInteractable(false);
                    // optionally change color/sprite to visited look
                    break;
                case NodeStates.Attainable:
                    SetInteractable(true);
                    break;
            }
        }

    }
}