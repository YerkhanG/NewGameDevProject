using global_events;
using map_encounter_system.level_system;
using persistence_system.manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace navigation_system
{
    public class LevelCreationManager : MonoBehaviour
    {
        //this manager just stores the level which after going into map scene is used by the map generation
        public LevelConfig CurrentLevel  => currentLevel;
        private LevelConfig currentLevel;
        private int currentFloor;
        public int CurrentFloor => currentFloor;
        public static LevelCreationManager Instance { get; private set; }

        public bool EnteredFinalNode = false;
        public void OnEnable()
        {
            GlobalEvents.OnWorldSelected += HandleWorldSelected;
        }

        public void OnDisable()
        {
            GlobalEvents.OnWorldSelected -= HandleWorldSelected;
        }
        public void Awake()
        {
            if (Instance != null) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        private void HandleWorldSelected(LevelConfig config)
        {
            currentLevel = config;
            SceneManager.LoadScene("MapScene");
        }
        public void SetCurrentFloor(int currentFloor)
        {
            this.currentFloor = currentFloor;
        }
        public void SetCurrentLevel(LevelConfig currentLevel)
        {
            this.currentLevel = currentLevel;
        }

        public void HandleWinningFloor()
        {
            Debug.Log("Won the floor");
            PersistenceManager.instance.ClearMapData();
            if (CheckForLevelWin())
            {
                //here i win completely so i should technically just fire the event? the event would be taken by the main ui manager and then after some dev time maybe i will add a reward for each unique "world"
                GlobalEvents.RaiseLevelCleared();
            }
            else
            {
                currentFloor++;
            }
        }
        public bool CheckForLevelWin()
        {
            if(currentFloor != currentLevel.floors)
            {
                return false;
            }
            return true;
        }
    }
}