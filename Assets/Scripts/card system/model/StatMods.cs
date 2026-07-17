using card_system.functionality.card_effect_types;

namespace model.entity_state
{
    public class StatMods
    {
        public StatModType type;
        public float amount;
        public int remainingTurns;

        public StatMods(StatModType type, float amount, int remainingTurns)
        {
            this.type = type;
            this.amount = amount;
            this.remainingTurns = remainingTurns;
        }
    }
}