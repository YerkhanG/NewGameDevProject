using System;
using System.Collections.Generic;
using UnityEngine;

namespace persistence_system.model
{
    [Serializable]
    public class MapDTO
    {
        public List<List<int>> nodeGrid;   // stores indices, preserving 2D layout
        public List<NodeDTO> allNodes;     // flat list of all nodes
        public List<Vector2Int> path;
        public string bossNodeName;
        public string configName;
    }
}