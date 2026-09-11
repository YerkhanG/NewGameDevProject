using System;
using global_events;
using model.entity;
using UnityEngine;

namespace card_system.functionality.passive_effects
{
    public class PassiveOnDamageController : PassiveEffectBase
    {
        public float perc = 0.1f;

        protected override void Awake()
        {
            base.Awake();
            duration = 1;
            owner = GetComponentInChildren<Entity>();
        }
        protected  override void OnEnable()
        {
            base.OnEnable();
            GlobalEvents.OnEntityDamageTaken += HandleEntityDamageTaken;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
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