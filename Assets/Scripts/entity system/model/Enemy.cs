using combat_system;
using enemyAI_system.functionality;
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
            var ai = GetComponentInParent<EnemyAi>();
            //for now only one mode at a time, so if both are true , only the first will work(idk about this)
            //alternative is that everything works , but mods restart every action(???)
            if (CombatEntityManager.instance.mainCharacter.currentHealth <=
                   CombatEntityManager.instance.mainCharacter.maxHealth / 2)
            {
                ai.ClearAllModes();
                ai.TriggerAggressiveMode();
            }
            else if (currentHealth <= maxHealth / 2)
            {
                ai.ClearAllModes();
                ai.TriggerDefensiveMode();
            }
            //final step to action taken
            ai.PickAndExecuteAbility();
        }
    }
}