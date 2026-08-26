using enemyAI_system.functionality;
using enemyAI_system.model;
using model.entity;
using UnityEngine;

namespace enemyAI_system.abilities
{
    public class DefaultShield : AbilityBase
    {
        public AbilityTypes AbilityType => AbilityTypes.Defense;
        public int amountToShield;
        public override void Execute(Entity ent)
        {
            Debug.Log("Enemy shields");
            ent.ShieldUp(amountToShield);
        }
    }
}