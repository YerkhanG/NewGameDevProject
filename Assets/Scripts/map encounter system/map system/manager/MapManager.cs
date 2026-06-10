using map_encounter_system.map_system.data;
using persistence_system.manager;
using persistence_system.model;
using UnityEngine;

namespace map_encounter_system.map_system.manager
{
    public class MapManager : MonoBehaviour
    {
        public MapGenerator mapGenerator;
        public MapView mapView;
        public Map map;
        public static MapManager instance;
        public Camera cam;
        public void Awake()
        {
            cam = Camera.main;
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
            /*MapSaveData mapData = PersistenceManager.instance.LoadSceneData();
            if (mapData.map != null)
            {
                Debug.Log("Loaded SceneData");
                map =  mapData.map;
            }
            else
            {
                Debug.Log("Saved SceneData");
                map =  mapGenerator.GenerateMap();
                MapSaveData data = new MapSaveData(map);
                PersistenceManager.instance.SaveSceneData(data);
            }*/
            mapView.ShowMap(map);
        }
    }
}