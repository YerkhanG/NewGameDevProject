using System.Collections.Generic;
using combat_system.UI;
using global_events;
using model.entity;
using UnityEngine;

namespace combat_system
{
    public class HealthBarManager : MonoBehaviour
    {
        public EntityHealthBarController enemyBar;
        public EntityHealthBarController playerBar;


        public Vector3  enemyBarOffset = new(0f, 3f , 0f);
        
        [SerializeField] private Transform enemyBarParent;              

        private List<EntityHealthBarController> activeEnemyBars = new();

        public void OnEnable()
        {
            GlobalEvents.onEncounterSpawned += RegisterEnemy;
            GlobalEvents.PlayerCreated += RegisterPlayer;
        }

        public void OnDisable()
        {
            GlobalEvents.onEncounterSpawned -= RegisterEnemy;
            GlobalEvents.PlayerCreated -= RegisterPlayer;
        }
        public void RegisterPlayer(Entity playerEntity)
        {
            playerBar.WireToEntity(playerEntity);
        }

        public void RegisterEnemy(List<Enemy>  enemies)
        {
            Debug.Log("Registering enemies" + enemies.Count);
            foreach (var enemy in enemies)
            {
                var bar = Instantiate(enemyBar, enemy.transform);
                bar.transform.localPosition = new Vector3(1f, 1f, 0);
                bar.WireToEntity(enemy);
                activeEnemyBars.Add(bar);

                // Clean up when enemy dies
                enemy.onDeath.AddListener(() =>
                {
                    activeEnemyBars.Remove(bar);
                    Destroy(bar.gameObject);
                });   
            }
        }
    }
}