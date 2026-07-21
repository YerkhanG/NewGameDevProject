using System;
using System.Collections.Generic;

namespace persistence_system.model
{
    [Serializable]
    public class CardInstanceRecord
    {
        public string instanceId;   // Guid.NewGuid().ToString() - identifies this specific copy
        public string templateId;   // same CardData asset ID you already save today
        public List<CardModification> modifications = new List<CardModification>();
    }
}