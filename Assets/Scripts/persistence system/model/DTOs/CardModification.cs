using System;

namespace persistence_system.model
{
    [Serializable]
    public class CardModification
    {
        public ModificationType type;
        public int effectIndex;          // which entry in cardEffects (FieldOverride / RemoveEffect)
        public string fieldName;         // e.g. "amountToShield" (FieldOverride)
        public float value;              // new value (FieldOverride)
        public string effectTemplateId;  // CardEffect asset ID to clone in (AddEffect)
    }
    
    
    public enum ModificationType { FieldOverride, AddEffect, RemoveEffect }
}