using combat_system;
using enemyAI_system.functionality;
using global_events;
using UnityEngine;

namespace model.entity
{
    public class Enemy : Entity
    {
        private EnemyAi enemyAi;

        protected override void Awake()
        {
            base.Awake();
            enemyAi = GetComponentInParent<EnemyAi>();
        } 
        protected override void Die()
        {
            base.Die();
            GlobalEvents.RaiseEnemyDied(this);
        }
        public virtual void TakeAction()
        {
            Debug.Log("Enemy takes action");
            
            //for now only one mode at a time, so if both are true , only the first will work(idk about this)
            //alternative is that everything works , but mods restart every action(???)
            if (CombatEntityManager.instance.mainCharacter.currentHealth <=
                   CombatEntityManager.instance.mainCharacter.maxHealth / 2)
            {
                enemyAi.ClearAllModes();
                enemyAi.TriggerAggressiveMode();
            }
            else if (currentHealth <= maxHealth / 2)
            {
                enemyAi.ClearAllModes();
                enemyAi.TriggerDefensiveMode();
            }
            //final step to action taken
            enemyAi.PickAndExecuteAbility();
        }
    }
}