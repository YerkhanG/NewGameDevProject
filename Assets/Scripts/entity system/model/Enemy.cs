using combat_system;
using UnityEngine;

namespace model.entity
{
    public class Enemy : Entity
    {
        public void TakeAction()
        {
            Debug.Log("Enemy takes action");
            var damage = GetTotalDamageBonus();
            CombatEntityManager.instance.mainCharacter.TakeDamage(damage);
        }
    }
}