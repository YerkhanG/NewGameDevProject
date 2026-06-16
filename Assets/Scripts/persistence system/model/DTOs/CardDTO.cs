using UnityEngine;

namespace persistence_system.model
{
    [CreateAssetMenu]
    public class CardDTO : ScriptableObject
    {
        public string id;
        public string cardName;
    }
}