using System.Collections.Generic;
using map_encounter_system.map_system.data.node;
using UnityEngine;

namespace map_encounter_system.map_system.data
{
    public class Map
    {
        public List<List<Node>> nodes;
        public List<Vector2Int> path;
        public string bossNodeName;
        public string configName; // similar to the act name in Slay the Spire

        public Map(string configName, string bossNodeName, List<List<Node>> nodes, List<Vector2Int> path)
        {
            this.configName = configName;
            this.bossNodeName = bossNodeName;
            this.nodes = nodes;
            this.path = path;
        }

    }
}