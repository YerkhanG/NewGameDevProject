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
            Map mapData = PersistenceManager.instance.LoadSceneData();
            if (mapData != null)
            {
                Debug.Log("Loaded SceneData");
                map =  mapData;
            }
            else
            {
                Debug.Log("Saved SceneData");
                map =  mapGenerator.GenerateMap();
                PersistenceManager.instance.SaveSceneData(map);
            }
            mapView.ShowMap(map);
        }
    }
}