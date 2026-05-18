using map_encounter_system.map_system.data;
using UnityEngine;

namespace map_encounter_system.map_system.manager
{
    public class MapManager : MonoBehaviour
    {
        public MapGenerator mapGenerator;
        public MapView mapView;

        void Start()
        {
            Map map =  mapGenerator.GenerateMap();
            mapView.ShowMap(map);
        }
    }
}