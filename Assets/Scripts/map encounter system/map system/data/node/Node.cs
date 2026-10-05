using System.Collections.Generic;
using encounter_system.data;
using UnityEngine;

namespace map_encounter_system.map_system.data.node
{
    [System.Serializable]
    public class Node
    {
        public Encounter.Rarity type;
        public Vector2 position;
        public Vector2Int gridPosition;
        public List<Vector2Int> connections2 = new();
        public bool isConnectedTo = false;
        public bool IsFinalNode = false;
        public void AddConnection(Node otherNode)
        {
            if (!connections2.Contains(otherNode.gridPosition))
            {
                connections2.Add(otherNode.gridPosition);
                otherNode.isConnectedTo = true;
            }
        }
        public Node(Encounter.Rarity type,Encounter data,  Vector2Int gridPosition) 
        {
            this.type = type;
            this.gridPosition = gridPosition;
        }
        public Node(Encounter.Rarity type, Vector2 position, Vector2Int gridPosition , bool IsFinalNode) 
        {
            this.type = type;
            this.gridPosition = gridPosition;
            this.position = position;
            this.IsFinalNode = IsFinalNode;
        }

        public bool HasNoConnections()
        {
            return connections2.Count == 0;
        }

    }
}