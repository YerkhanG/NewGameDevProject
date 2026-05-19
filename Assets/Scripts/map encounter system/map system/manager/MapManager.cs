using map_encounter_system.map_system.data;
using UnityEngine;

namespace map_encounter_system.map_system.manager
{
    public class MapManager : MonoBehaviour
    {
        public MapGenerator mapGenerator;
        public MapView mapView;
        public Map map;
        public static MapManager instance;
        
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            map =  mapGenerator.GenerateMap();
            mapView.ShowMap(map);
        }
    }
}