using UnityEngine;

namespace entity_system.spawners
{
    public class EnemySpawnerManager : MonoBehaviour
    {
        public static  EnemySpawnerManager instance;
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

        public void SpawnRandomEncounter()
        {
            
        }
        
    }
}