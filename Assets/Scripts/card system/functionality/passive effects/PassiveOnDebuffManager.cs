using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using card_effect_types;
using global_events;
using model.entity;
using UnityEngine;
namespace passive_effects
{
    public class PassiveOnDebuffManager : PassiveEffectBase
    {
        [SerializeField]private float buffIncrement;
        [SerializeField]private float maxDmgBuff;
        private float currentDmgBuff;
        protected override void Awake()
        {
            base.Awake();
            owner = GetComponentInChildren<Entity>();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GlobalEvents.OnDebuffApplied -= HandleDebuffApplied;
            currentDmgBuff = 0;
        }

        private void HandleDebuffApplied()
        {
            currentDmgBuff += buffIncrement;
            if(currentDmgBuff < maxDmgBuff)
            {
                owner.AddBuff(StatModType.Damage , buffIncrement , duration);
            }
            else
            {
                Debug.Log("maximum buff amount : Debuff Passive");
            }
        }
    }
}