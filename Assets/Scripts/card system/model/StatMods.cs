using card_system.functionality.card_effect_types;

namespace model.entity_state
{
    public class StatMods
    {
        public StatModType type;
        public int amount;
        public int remainingTurns;

        public StatMods(StatModType type, int amount, int remainingTurns)
        {
            this.type = type;
            this.amount = amount;
            this.remainingTurns = remainingTurns;
        }
    }
}