using enemyAI_system.functionality;
using enemyAI_system.model;
using model.entity;

namespace enemyAI_system.interfaces
{
    public interface IAbility
    {
        public AbilityTypes AbilityType { get; }
        public void TickCooldown();
        public int GetCurrentWeight();
        public void SetCurrentWeight(int weight);
        public void Execute(Entity caster);
        public bool IsAvailable();
    }
}