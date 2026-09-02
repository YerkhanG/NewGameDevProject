using UnityEngine;
namespace card_system.functionality.card_effect_types
{
    [CreateAssetMenu(fileName = "New Card Effect Data", menuName = "Card Effect/Heal on Damage Effect Data")]
    public class HealOnDamageEffect : CardEffect
    {
        public override void Execute(EffectContext context)
        {
            var passive = context.caster.gameObject.AddComponent<passive_effects.PassiveOnDamageController>();
        }
    }
}