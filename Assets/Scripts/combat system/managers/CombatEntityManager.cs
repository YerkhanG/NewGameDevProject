using System;
using System.Collections.Generic;
using global_events;
using model.entity;
using UnityEngine;
using UnityEngine.XR;

namespace combat_system
{
    //this is for keeping track of all entities in the scene. At least for now.
    public class CombatEntityManager : MonoBehaviour
    {
        public List<Enemy> enemies = new List<Enemy>();
        public Entity mainCharacter;
        public static  CombatEntityManager instance;

        private void OnEnable()
        {
            GlobalEvents.OnEnemyDied += HandleEnemyDied;
        }

        void OnDisable()
        {
            GlobalEvents.OnEnemyDied -= HandleEnemyDied;
        }

        private void HandleEnemyDied(Enemy enemy)
        {
            Debug.Log("Checking the enemies");
            enemies.Remove(enemy);
            if (enemies.Count == 0)
            {
                GlobalEvents.RaiseFightWon();
            }
        }

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

        public bool CheckMC()
        {
            return mainCharacter.IsAlive;
        }

        public bool CheckEnemies()
        {
            return enemies.Count > 0;
        }
        public List<Enemy> GetAllEnemies()
        {
            return enemies;
        }

        public void UpdateBuffsAndDebuffs()
        {
            mainCharacter.UpdateStatMods();
            foreach(Enemy enemy in enemies)
            {
                enemy.UpdateStatMods();
            }
        }
    }
}