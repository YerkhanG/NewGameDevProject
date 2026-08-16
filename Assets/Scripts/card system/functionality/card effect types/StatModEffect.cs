using System.Collections.Generic;
using model.entity;
using UnityEngine;
//TODO: need to add a level up and add effect???(idk)
namespace card_system.functionality.card_effect_types
{
    [CreateAssetMenu(fileName = "New Card Effect Data", menuName = "Card Effect/Buff Effect Data")]
    public class StatModEffect : CardEffect
    {
        public StatModType type;
        public float amount;
        public int duration;
        
        
        //TODO: I need to make this a bit more flexible for two fields 
        /*public override bool HasField(string fieldName) => fieldName == nameof(duration);
        public  bool HasField(string fieldName) => fieldName == nameof(amount);*/
        public override void ApplyFieldOverride(string fieldName, float value)
        {
            if (fieldName == nameof(duration))
                duration += Mathf.RoundToInt(value);
        }
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