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
        public override void Execute( EffectContext context)
        {
            List<Entity> targets = ResolveTargets(context, targetType);
            foreach (Entity target in targets)
            {
                if (target != null)
                {
                    int damage = context.caster.GetTotalDamageBonus();
                    target.TakeDamage(damage);
                    GlobalEvents.RaiseAttackEffectPlayed(target);
                    Debug.Log($"Dealt {damage} damage to {target.name}");
                }
            }
        }
    }
}