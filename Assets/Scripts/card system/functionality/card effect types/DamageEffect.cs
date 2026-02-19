using global_events;
using model.entity;
using UnityEngine;

namespace card_system.functionality.card_effect_types
{
    [CreateAssetMenu(fileName = "New Card Effect Data", menuName = "Card Effect/Damage Effect Data")]
    public class DamageEffect : CardEffect
    {
        public float damageAmount;
        public override void Execute( Entity target = null)
        {
            Debug.Log("Played DamageEffect 12");
            target.currentHealth -= damageAmount;
            GlobalEvents.RaiseAttackEffectPlayed(target);
        }
    }
}