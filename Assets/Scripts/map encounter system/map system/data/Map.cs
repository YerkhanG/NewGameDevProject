using System.Collections.Generic;
using System.Linq;
using encounter_system.data;
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
        
        public float DistanceBetweenFirstAndLastLayers()
        {
            /*Node bossNode = GetBossNode();*/
            Node firstLayerNode = nodes[0].FirstOrDefault(n => n.position.y  == 0);

            if (/*bossNode == null ||*/ firstLayerNode == null)
                return 0f;

            return/* bossNode.position.y - */firstLayerNode.position.y;
        }
        /*public Node GetBossNode()
        {
            /*return nodes.FirstOrDefault(n => n.FirstOrDefault(x => x.type == Encounter.Rarity.Elite));#1#
        }*/

    }
}