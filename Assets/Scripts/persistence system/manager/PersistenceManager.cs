using persistence_system.model;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
namespace persistence_system.manager
{
    public class PersistenceManager : MonoBehaviour
    {
        public static PersistenceManager instance;
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
            DontDestroyOnLoad(gameObject);
        }
        public void SaveSceneData(MapSaveData saveData)
        {
            //Save map , playterstate
            string json =  JsonConvert.SerializeObject(saveData);
            File.WriteAllText(Application.persistentDataPath + "/sceneSaveData.json", json);
            Debug.Log("Game Saved!");
        }

        public void SaveSessionData()
        {
            //unclokced stuff 
            
        }

        public void LoadSessionData()
        {
            
        }

        public MapSaveData LoadSceneData()
        {
            string path = Application.persistentDataPath + "/sceneSaveData.json";
            if (File.Exists(path))
            {
                string json =  File.ReadAllText(path);
                MapSaveData data = JsonUtility.FromJson<MapSaveData>(json);
                Debug.Log("Game Loaded!");
                return data;
            }
            else
            {
                return null;
            }
        }
}
}