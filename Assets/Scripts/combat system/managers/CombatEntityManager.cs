using System;
using System.Collections.Generic;
using encounter_system.data;
using encounter_system.managers;
using global_events;
using map_encounter_system.encounter_system.scene_persistance;
using model.entity;
using UnityEngine;
using UnityEngine.XR;

namespace combat_system
{
    //this is for keeping track of all entities in the scene. At least for now.
    public class CombatEntityManager : MonoBehaviour
    {
        public List<Enemy> enemies = new List<Enemy>();
        public List<Transform> enemiesTransforms = new List<Transform>();
        public Entity mainCharacter;
        public static  CombatEntityManager instance;
        //Maybe unecessary
        public Encounter encounterData;
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

        private void Start()
        {
            Encounter enc = EncounterData.instance.currentEncounter;
    
            for (int i = 0; i < enc.enemies.Count; i++)
            {
                Instantiate(enc.enemies[i].gameObject, enemiesTransforms[i]);
                enemies.Add(enc.enemies[i].GetComponent<Enemy>());
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