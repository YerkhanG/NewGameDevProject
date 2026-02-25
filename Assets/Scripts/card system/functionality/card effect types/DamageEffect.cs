using System.Collections.Generic;
using combat_system;
using global_events;
using model.entity;
using UnityEngine;

namespace card_system.functionality.card_effect_types
{
    [CreateAssetMenu(fileName = "New Card Effect Data", menuName = "Card Effect/Damage Effect Data")]
    public class DamageEffect : CardEffect
    {
        // TODO: change this to a percentage of mcs attack
        public float damageAmount;
        public override void Execute( EffectContext context)
        {
            List<Entity> targets = ResolveTargets(context, targetType);
            foreach (Entity target in targets)
            {
                if (target != null)
                {
                    target.TakeDamage(damageAmount);
                    Debug.Log($"Dealt {damageAmount} damage to {target.name}");
                }
            }
        }
    }
}