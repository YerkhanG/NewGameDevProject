using data;
using UnityEngine;

namespace model.entity
{
    public class Entity : MonoBehaviour
    {
        private float currentHealth;
        private float baseDamage;
        private float armor;
        private float maxHealth;
        public  EntityData data;

        private void Awake()
        {
            maxHealth = data.health;
            currentHealth = maxHealth;
            baseDamage = data.baseDamage;
            armor = data.armor;
        }
    }
}