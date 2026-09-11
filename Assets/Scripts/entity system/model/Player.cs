using System;
using global_events;
using persistence_system.manager;
using persistence_system.model;
using UnityEditor;
using UnityEngine;

namespace model.entity
{
    public class Player : Entity
    {
        private new void Awake()
        {
            LoadedData loadedData = PersistenceManager.instance.LoadSceneData();
        
            if (loadedData?.playerState != null)
            {
                IsAlive = true;
                maxHealth = loadedData.playerState.maxHealth;
                currentHealth = loadedData.playerState.health;
                baseDamage = loadedData.playerState.baseDamage;
            }
            else
            {
                base.Awake(); // fall back to EntityData defaults
            }
        }
        public void Onable()
        {
            GlobalEvents.OnPlayerTurnStarted += HandlePlayerTurnStarted;
        }
        public void OnDisable()
        {
            GlobalEvents.OnPlayerTurnStarted -= HandlePlayerTurnStarted;
        }

        public void HandlePlayerTurnStarted()
        {
            ResetShield();
        }
    }
}