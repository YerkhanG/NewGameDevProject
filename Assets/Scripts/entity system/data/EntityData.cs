using UnityEngine;

namespace data
{
    [CreateAssetMenu(fileName = "New Entity Data", menuName = "Entity/Entity Data")]
    public class EntityData : ScriptableObject
    {
        public int health;
        public int baseDamage;
        public int armor;
    }
}