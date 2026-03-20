using model.entity;
using UnityEngine;

namespace combat_system
{
    public class EnemyAIManager : MonoBehaviour
    {
        public static  EnemyAIManager instance;
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AllEnemiesTurn()
        {
            foreach (Enemy enemy in CombatEntityManager.instance.enemies)
            {
                enemy.TakeAction();
            }
        }
    }
}