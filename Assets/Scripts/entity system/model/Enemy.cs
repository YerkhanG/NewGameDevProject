using combat_system;
using UnityEngine;

namespace model.entity
{
    public class Enemy : Entity
    {
        public void TakeAction()
        {
            Debug.Log("Enemy takes action");
            CombatEntityManager.instance.mainCharacter.TakeDamage(baseDamage);
        }
    }
}