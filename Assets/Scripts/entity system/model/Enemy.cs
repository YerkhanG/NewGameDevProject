using combat_system;
using global_events;
using UnityEngine;

namespace model.entity
{
    public class Enemy : Entity
    {
        protected override void Die()
        {
            base.Die();
            GlobalEvents.RaiseEnemyDied(this);
        }
        public void TakeAction()
        {
            Debug.Log("Enemy takes action");
            var damage = GetTotalDamageBonus();
            CombatEntityManager.instance.mainCharacter.TakeDamage(damage);
        }
    }
}