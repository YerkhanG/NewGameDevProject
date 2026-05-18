using System.Collections.Generic;
using encounter_system.data;
using UnityEditor;
using UnityEngine;

namespace map_encounter_system.map_system.data.node
{
    public class Node
    {
        public Encounter.Rarity type;
        public Encounter encounterData; // Reference to the "Recipe"
        public Vector2 position;
        public List<Node> connections = new List<Node>();
        public bool isConnectedTo = false;
        public void AddConnection(Node otherNode)
        {
            if (!connections.Contains(otherNode))
            {
                connections.Add(otherNode);
            }
        }
        public Node(Encounter.Rarity type, Encounter data, Vector2 pos) 
        {
            this.type = type;
            encounterData = data;
            position = pos;
        }

        public bool HasNoConnections()
        {
            return connections.Count == 0;
        }

    }
}