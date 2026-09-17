using encounter_system.data;
using math_structs;
using UnityEngine;

namespace map_encounter_system.map_system
{
    [System.Serializable]
    public class MapLayer
    {
        public float nodesApartDistance;
        public FloatMinMax DistanceFromPreviousLayer;
        public Encounter.Rarity defaultNodeType;
        [Range(0f, 1f)] public float randomizeNodes;
    }
}