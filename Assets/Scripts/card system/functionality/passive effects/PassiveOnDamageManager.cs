using System;
using global_events;
using model.entity;
using UnityEngine;

namespace card_system.functionality.passive_effects
{
    public class PassiveOnDamageController : MonoBehaviour
    {
        public Entity owner;
        [SerializeField] private float perc = 0.1f;

        public void Awake()
        {
            owner = GetComponentInChildren<Entity>();
        }
        public void OnEnable()
        {
            GlobalEvents.OnEntityDamageTaken += HandleEntityDamageTaken;
        }

        public void OnDisable()
        {
            GlobalEvents.OnEntityDamageTaken -= HandleEntityDamageTaken;
        }
        private void HandleEntityDamageTaken(Entity ent, int amount)
        {
            if (ent.GetComponentInChildren<Enemy>() != null)
            {
                owner.Heal((int)(amount * perc));
            } 
        }
    }
}