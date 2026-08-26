using enemyAI_system.functionality;
using enemyAI_system.model;
using model.entity;
using UnityEngine;

namespace enemyAI_system.abilities
{
    public class DefaultHeal : AbilityBase
    {
        public AbilityTypes AbilityType => AbilityTypes.Defense;
        public int amountOfHealing;
        public override void Execute(Entity ent)
        {
            Debug.Log("Enemy heals");
            ent.Heal(amountOfHealing);
        }
    }
}