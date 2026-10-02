using map_encounter_system.map_system.data;
using navigation_system;
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
            //unoptimized bullshit
            LoadedData loadedData = PersistenceManager.instance.LoadSceneData();
            /*Map mapData = PersistenceManager.instance.LoadSceneData();*/
            if (loadedData != null && loadedData.loadedMap != null)
            {
                Debug.Log("Loaded SceneData");
                map = loadedData.loadedMap;
            }
            else
            {
                Debug.Log("Saved SceneData");
                if(WorldCreationManager.Instance == null) Debug.Log("bullshit with instance");
                if(WorldCreationManager.Instance.CurrentLevel == null) Debug.Log("problem with config");
                mapGenerator.config = WorldCreationManager.Instance.CurrentLevel.mapConfig;
                map =  mapGenerator.GenerateMap();
                
                PersistenceManager.instance.SaveSceneData( map : map, levelConfigId : WorldCreationManager.Instance.CurrentLevel.Id, currentFloor : WorldCreationManager.Instance.CurrentFloor.ToString());
            }
            mapView.ShowMap(map);
            mapView.UpdateNodeStates(map);
        }
    }
}