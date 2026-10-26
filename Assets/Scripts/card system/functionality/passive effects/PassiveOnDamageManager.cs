using System;
using global_events;
using model.entity;
using UnityEngine;

namespace passive_effects
{
    public class PassiveOnDamageManager : PassiveEffectBase
    {
        public float perc = 0.1f;
        protected override void Awake()
        {
            base.Awake();
            owner = GetComponentInChildren<Entity>();
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