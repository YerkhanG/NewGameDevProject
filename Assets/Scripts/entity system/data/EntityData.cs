using UnityEngine;

namespace data
{
    [CreateAssetMenu(fileName = "New Entity Data", menuName = "Entity/Entity Data")]
    public class EntityData : ScriptableObject
    {
        public float health;
        public float baseDamage;
        public float armor;
    }
}