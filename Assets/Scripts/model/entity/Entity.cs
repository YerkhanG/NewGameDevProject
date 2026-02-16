using data;
using UnityEngine;

namespace model.entity
{
    public class Entity : MonoBehaviour , IDamageable
    {
        public float currentHealth;
        public float baseDamage;
        public float armor;
        public float maxHealth;
        public  EntityData data;

        private void Awake()
        {
            maxHealth = data.health;
            currentHealth = maxHealth;
            baseDamage = data.baseDamage;
            armor = data.armor;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0) Die();
        }

        private void Die()
        {
            Destroy(this.gameObject);
        }

        public bool IsAlive { get; }
    }
}