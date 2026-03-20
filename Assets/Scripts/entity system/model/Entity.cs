using System.Collections.Generic;
using card_system.functionality.card_effect_types;
using data;
using model.entity_state;
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
        public List<Buff> activeBuffs = new List<Buff>();

        public int TotalDamage => GetTotalDamageBonus();
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
            Debug.Log("It died");
            IsAlive = false;
            Destroy(transform.parent.gameObject);
        }

        public bool IsAlive { get; private set; }
        
        public void AddBuff(BuffType type, float amount, int duration)
        {
            Buff newBuff = new Buff(type, amount, duration);
            activeBuffs.Add(newBuff);
            Debug.Log($"{name} gained {amount} {type} for {duration} turns");
        }
        
        public void UpdateBuffs()
        {
            for (int i = activeBuffs.Count - 1; i >= 0; i--)
            {
                activeBuffs[i].remainingTurns--;
                if (activeBuffs[i].remainingTurns <= 0)
                {
                    Debug.Log($"{name} lost buff: {activeBuffs[i].type}");
                    activeBuffs.Remove(activeBuffs[i]);
                }
            }
        }
        public int GetTotalDamageBonus()
        {
            float total = baseDamage;
            if (activeBuffs.Count > 0)
            {
                foreach (var buff in activeBuffs)
                {
                    if (buff.type == BuffType.Damage)
                        total *= buff.amount;
                }   
            }
            else
            {
                Debug.Log("No buffs");
            }
            return Mathf.RoundToInt(total);
        }
    }
}