using model.entity;
using UnityEngine;

namespace card_system.functionality
{
    [CreateAssetMenu(fileName = "New Card Effect Data", menuName = "Card Effect/Card Effect Data")]
    public abstract class CardEffect : ScriptableObject
    {
        public abstract void Execute( Entity target = null);
    }
}