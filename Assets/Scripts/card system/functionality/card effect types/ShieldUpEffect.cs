using System.Collections.Generic;
using model.entity;
using UnityEngine;

namespace card_system.functionality.card_effect_types
{
    //TODO: needs to be tested
    [CreateAssetMenu(fileName = "New Card Effect Data", menuName = "Card Effect/Shield Up Effect Data")]
    public class ShieldUpEffect : CardEffect
    {
        public int amountToShield;
        
        //TODO: in theory should work , should test later
        
        public override bool HasField(string fieldName) => fieldName == nameof(amountToShield);

        public override void ApplyFieldOverride(string fieldName, float value)
        {
            if (fieldName == nameof(amountToShield))
                amountToShield += Mathf.RoundToInt(value);
        }
        public override void Execute(EffectContext context)
        {
            List<Entity> targets = ResolveTargets(context, targetType);
            foreach (Entity target in targets)
            {
                if (target != null)
                {
                    target.ShieldUp(amountToShield);
                }
            }
        }
    }
}