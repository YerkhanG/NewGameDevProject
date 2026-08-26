using combat_system;
using enemyAI_system.functionality;
using enemyAI_system.model;
using model.entity;
using UnityEngine;

namespace enemyAI_system.abilities
{
    public class DefaultAttack : AbilityBase
    {
        public AbilityTypes AbilityType => AbilityTypes.Damage;

        public override void Execute(Entity ent)
        {
            Debug.Log("Enemy atacks");
            CombatEntityManager.instance.mainCharacter.TakeDamage(ent.baseDamage);
        }
    }
}