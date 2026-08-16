using System.Collections.Generic;
using global_events;
using model.entity;
using UnityEngine;
//TODO: need to add a level up and add effect
namespace card_system.functionality.card_effect_types
{
    [CreateAssetMenu(fileName = "New Card Effect Data", menuName = "Card Effect/Heal Effect Data")]
    public class HealEffect : CardEffect
    {
        public int amountToHeal;
        
        public override bool HasField(string fieldName) => fieldName == nameof(amountToHeal);

        public override void ApplyFieldOverride(string fieldName, float value)
        {
            if (fieldName == nameof(amountToHeal))
                amountToHeal += Mathf.RoundToInt(value);
        }
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