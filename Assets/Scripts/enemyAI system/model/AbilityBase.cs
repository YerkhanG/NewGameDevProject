using enemyAI_system.functionality;
using enemyAI_system.interfaces;
using model.entity;
using UnityEngine;

namespace enemyAI_system.model
{
    public abstract class AbilityBase : MonoBehaviour, IAbility
    {
        public int baseWeight;
        public int cooldown = 1;
        public int currentCooldown = 0;
        public AbilityTypes AbilityType { get; set; }
        public void TickCooldown()
        {
            if (currentCooldown > 0)
            {
                currentCooldown--;
            }
        }

        public int GetCurrentWeight()
        {
            return baseWeight;
        }

        public abstract void Execute(Entity caster);

        bool IAbility.IsAvailable() => currentCooldown == 0;
        
        protected void ResetCooldown()
        {
            currentCooldown = cooldown;
        }

        public void SetCurrentWeight(int weight)
        {
            baseWeight = weight;
        }
    }
}