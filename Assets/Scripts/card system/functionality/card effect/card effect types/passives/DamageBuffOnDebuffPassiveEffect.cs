using passive_effects;
using UnityEngine;
namespace card_effect_types
{
    [CreateAssetMenu(fileName = "New Card Effect Data", menuName = "Card Passive Effect/Dmg Buff on Debuff Effect Data")]
    public class DamageBuffOnDebuffPassiveEffect : CardEffect
    {
        public int duration;
        public override void Execute(EffectContext context)
        {
            var passive = context.caster.gameObject.AddComponent<PassiveOnDebuffManager>();
            passive.Initialize(duration);
        }
    }
}