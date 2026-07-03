using global_events;
using persistence_system.manager;
using persistence_system.model;
using UnityEngine;

namespace metaprogression_system.managers
{
    //TODO: Still dont know about this coin manager , but we will see 
    public class SoulCoinManager : MonoBehaviour
    {
        public static  SoulCoinManager instance;
        
        private int SoulCoinCount = 0;
        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Start()
        {
            SessionData loadedData = PersistenceManager.instance.LoadSessionData();
            if (loadedData != null && loadedData.currency > 0)
                SoulCoinCount = loadedData.currency;
    
            GlobalEvents.RaiseCurrencyChanged(SoulCoinCount); // UI picks this up
        }
        public void SetSoulCoinCount(int soulCoinCount)
        {
            SoulCoinCount = soulCoinCount;
        }
        public int  GetSoulCoinCount()
        {
            return SoulCoinCount;
        }
        
        public void AddSoulCoins(int amount)
        {
            SoulCoinCount += amount;
            GlobalEvents.RaiseCurrencyChanged(SoulCoinCount);
            Save();
        }

        public bool TrySpend(int amount)
        {
            if (SoulCoinCount < amount) return false;
            SoulCoinCount -= amount;
            GlobalEvents.RaiseCurrencyChanged(SoulCoinCount);
            Save();
            return true;
        }
        
        private void Save()
        {
            PersistenceManager.instance.SaveSessionData(new SessionData { currency = SoulCoinCount });
        }
    }
}