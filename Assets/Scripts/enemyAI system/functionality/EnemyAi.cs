using System.Collections.Generic;
using System.Linq;
using combat_system;
using enemyAI_system.interfaces;
using enemyAI_system.model;
using model.entity;
using UnityEngine;
using Random = System.Random;

namespace enemyAI_system.functionality
{
    public class EnemyAi : MonoBehaviour
    {
        public List<IAbility> abilities = new();
        public Entity enemyEntity;
        public int modVar = 2;
        private int cnt = 0;
        private int pickedWeight = 0;
        
        
        private bool dmgFlag = false;
        private bool defFlag = false;
        private bool supFlag = false;
        private void Awake()
        {
            abilities = GetComponents<IAbility>().ToList();
            enemyEntity = GetComponentInChildren<Enemy>();
        }

        public void PickAndExecuteAbility()
        {
            cnt = 0;
            foreach (var c in abilities)
            {
                c.TickCooldown();       
            }
            //i have to now pick an abilkity based on weight and also include in that picking only available abilities
            //pick a random number in a sum of available abilities
            var f = abilities.Where(a => a.IsAvailable()).ToList();
            var chosen = GetRandomAvailable(f);
            chosen.Execute(enemyEntity);
        }

        public IAbility GetRandomAvailable(List<IAbility> available)
        {
            int totalWeight = available.Sum(a => a.GetCurrentWeight());
            int roll = UnityEngine.Random.Range(0, totalWeight);
            var cum = 0;
            foreach (var c in available)
            {
                cum += c.GetCurrentWeight();
                if (roll < cum)
                {
                    return c;
                }
            }

            return available.Last();
        }

        public void TriggerAggressiveMode()
        {
            dmgFlag = true;
            foreach (var c in abilities)
            {
                if (c.AbilityType == AbilityTypes.Damage)
                {
                    c.SetCurrentWeight(c.GetCurrentWeight() * modVar);
                }
            }
        }

        public void TriggerDefensiveMode()
        {
            defFlag = true;
            foreach (var c in abilities)
            {
                if (c.AbilityType == AbilityTypes.Defense)
                {
                    c.SetCurrentWeight(c.GetCurrentWeight() * modVar);
                }
            }
        }

        public void ClearAllModes()
        {
            if (dmgFlag)
            {
                foreach (var c in abilities)
                {
                    if (c.AbilityType == AbilityTypes.Damage)
                    {
                        c.SetCurrentWeight(c.GetCurrentWeight() / modVar);
                    }
                }
            }
            if (defFlag)
            {
                foreach (var c in abilities)
                {
                    if (c.AbilityType == AbilityTypes.Defense)
                    {
                        c.SetCurrentWeight(c.GetCurrentWeight() / modVar);
                    }
                }
            }
        }
    }
}