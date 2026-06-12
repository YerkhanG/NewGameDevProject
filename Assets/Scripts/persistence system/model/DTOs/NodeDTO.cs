using System;
using System.Collections.Generic;
using encounter_system.data;
using UnityEngine;

namespace persistence_system.model
{
    [Serializable]
    public class NodeDTO
    {
        public Encounter.Rarity type;
        public Vector2 position;
        public List<int> connectionIndices;  // indices into the flat node list
        public bool isConnectedTo;
    }
}