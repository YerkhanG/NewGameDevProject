using global_events;
using UnityEngine;

namespace combat_system
{
    //this will be firing some events to change the UI, and will also be consuming some to change the mana count in the backend
    public class ManaCountManager : MonoBehaviour
    {
        public static ManaCountManager instance;
        private int manaCount;
        public int maxMana = 10;
        public void Awake()
        {
            manaCount = maxMana;
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public bool HasEnoughMana(int cost) => cost <= manaCount;

        public bool TryToSpendMana(int cost)
        {
            if (!HasEnoughMana(cost)) return false;
            manaCount -= cost;
            GlobalEvents.RaiseManaChanged(manaCount);
            return true;
        }

        public int GetManaCount() => manaCount;

        private void IncreaseManaCount(int addedManaCount)
        {
            manaCount += addedManaCount;
        }
    }
}