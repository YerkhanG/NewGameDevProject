using UnityEngine;

namespace persistence_system.model
{
    [CreateAssetMenu]
    public class CardData : ScriptableObject
    {
        public string id;
        public string cardName;
    }
}