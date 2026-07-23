using persistence_system.model;
using UnityEngine;

namespace card_modification_system.data
{
    [CreateAssetMenu(fileName = "New Mod Definition", menuName = "Card Modification/Mod Definition")]
    public class ModDefinition : ScriptableObject
    {
        public string id;
        [TextArea] public string descriptionTemplate;   // e.g. "+{0} Shield"
        public ModificationType type;
        public int effectIndex;
        public string fieldName;
        public float value;
        public string effectTemplateIdForAdd;            // only used for AddEffect

        public string GetDescription() => string.Format(descriptionTemplate, value);
    }
}