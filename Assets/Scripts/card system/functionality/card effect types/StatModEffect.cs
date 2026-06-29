using System.Collections.Generic;
using model.entity;
using UnityEngine;

namespace card_system.functionality.card_effect_types
{
    [CreateAssetMenu(fileName = "New Card Effect Data", menuName = "Card Effect/Buff Effect Data")]
    public class StatModEffect : CardEffect
    {
        public StatModType type;
        public int amount;
        public int duration;
        public override void Execute(EffectContext context)
        {
            List<Entity> targets = ResolveTargets(context, targetType);
            foreach(Entity target in targets )
            {
                target.AddBuff(type, amount, duration);
            }
        }
    }

    public enum StatModType
    {
        Health, Damage, armor, 
    }
}