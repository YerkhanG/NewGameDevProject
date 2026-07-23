using System.Collections.Generic;
using card_system.data;
using persistence_system.manager;
using persistence_system.model;
using UnityEngine;

namespace metaprogression_system.managers
{
    public class SessionManager : MonoBehaviour
    {
        public static SessionManager instance;
        private SessionData sessionData;

        private void Awake()
        {
            if (instance == null) instance = this;
            else { Destroy(gameObject); return; }
            DontDestroyOnLoad(gameObject);
        }
        private void Start()
        {
            sessionData = PersistenceManager.instance.LoadSessionData() ?? new SessionData();
    
            if (sessionData.unlockedCardIds.Count == 0)
                sessionData.unlockedCardIds = new List<string> { "aoe_strike" , "buff2" , "buff" , "double_damage" , "single_strike", "shield_up" };
        }
        // Currency
        public int GetCurrency() => sessionData.currency;
        public void AddCurrency(int amount)
        {
            sessionData.currency += amount;
            Save();
        }
        public bool TrySpend(int amount)
        {
            if (sessionData.currency < amount) return false;
            sessionData.currency -= amount;
            Save();
            return true;
        }

        // Unlocks
        public List<string> GetUnlockedCardIds() => sessionData.unlockedCardIds;
        public void UnlockCard(string id)
        {
            if (sessionData.unlockedCardIds.Contains(id)) return;
            sessionData.unlockedCardIds.Add(id);
            Save();
        }
        public bool IsCardUnlocked(string id) => sessionData.unlockedCardIds.Contains(id);

        private void Save()
        {
            PersistenceManager.instance.SaveSessionData(sessionData);
        }
    }
}