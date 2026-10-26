using passive_effects;
using UnityEngine;
namespace card_effect_types
{
    [CreateAssetMenu(fileName = "New Card Effect Data", menuName = "Card Passive Effect/Heal on Damage Effect Data")]
    public class HealOnDamagePassiveEffect : CardEffect
    {
        public int duration;
        public override void Execute(EffectContext context)
        {
            var passive = context.caster.gameObject.AddComponent<PassiveOnDamageManager>();
            passive.Initialize(duration);
        }
    }
}