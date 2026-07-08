using System.Collections.Generic;
using global_events;
using model.entity;
using UnityEngine;

namespace card_system.functionality.card_effect_types
{
    [CreateAssetMenu(fileName = "New Card Effect Data", menuName = "Card Effect/Heal Effect Data")]
    public class HealEffect : CardEffect
    {
        public int amountToHeal;
        public override void Execute(EffectContext context)
        {
            List<Entity> targets = ResolveTargets(context, targetType);
            foreach (Entity target in targets)
            {
                if (target != null)
                {
                    target.Heal(amountToHeal);
                }
            }
        }
    }
}