using System;
using System.Collections.Generic;

namespace persistence_system.model
{
    [Serializable]
    public class SessionData
    {
        public int currency;
        public List<string> unlockedCardIds = new();
    }
}