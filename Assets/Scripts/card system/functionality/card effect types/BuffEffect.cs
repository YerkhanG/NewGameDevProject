using UnityEngine;

namespace card_system.functionality.card_effect_types
{
    [CreateAssetMenu(fileName = "New Card Effect Data", menuName = "Card Effect/Buff Effect Data")]
    public class BuffEffect : CardEffect
    {
        //TODO: Probably list of buffs that can be applied by one effect , or maybe one buff per effect.
        //Another struct to represent different types of buffs.
        //We will see how bad it will get. And then after I have to somehow validate the target , and move the card back if the target is wrongly chosen.
        //At least in plan 
        public override void Execute(EffectContext context)
        {
            
        }
    }
}