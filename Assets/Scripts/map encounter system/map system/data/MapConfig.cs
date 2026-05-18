using System.Collections.Generic;
using encounter_system.data;
using map_encounter_system.map_system.data.node;
using math_structs;
using UnityEngine;

namespace map_encounter_system.map_system.data
{
    [CreateAssetMenu(fileName = "NewConfig", menuName = "Map/Config")]
    public class MapConfig : ScriptableObject
    {
        public List<MapLayer> layers;
        public int widthNodeCount;
        public int layerCount;
        

        public IntMinMax numOfPreBossNodes;
        public IntMinMax numOfStartingNodes;
        [Tooltip("Increase this number to generate more paths")]
        public int extraPaths;
        /*public List mapNodes = new List<MapNode>();*/
        public List<Encounter.Rarity> randomTypes = new List<Encounter.Rarity>{Encounter.Rarity.Normal, Encounter.Rarity.Hard, Encounter.Rarity.Elite};
    }
}