using System.Collections.Generic;
using card_system.functionality.card_effect_types;
using data;
using model.entity_state;
using UnityEngine;

namespace model.entity
{
    public class Entity : MonoBehaviour , IDamageable
    {
        public int currentHealth;
        public int baseDamage;
        public int armor;
        public int maxHealth;
        public  EntityData data;
        public List<StatMods> activeStatMods = new List<StatMods>();
        protected bool isDead = false;
        public int TotalDamage => GetTotalDamageBonus();
        protected void Awake()
        {
            IsAlive = true;
            maxHealth = data.health;
            currentHealth = maxHealth;
            baseDamage = data.baseDamage;
            armor = data.armor;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0) Die();
        }

        public void Heal(int amount)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        }
        protected virtual void Die()
        {
            if (isDead) return;
            isDead = true;
            Debug.Log(name + " died");
            IsAlive = false;
            Destroy(transform.parent.gameObject);
        }

        public bool IsAlive { get; protected set; }
        
        public void AddBuff(StatModType type, int amount, int duration)
        {
            StatMods newStatMods = new StatMods(type, amount, duration);
            activeStatMods.Add(newStatMods);
            Debug.Log($"{name} gained {amount} {type} for {duration} turns");
        }
        public void UpdateStatMods()
        {
            for (int i = activeStatMods.Count - 1; i >= 0; i--)
            {
                activeStatMods[i].remainingTurns--;
                if (activeStatMods[i].remainingTurns <= 0)
                {
                    Debug.Log($"{name} lost buff: {activeStatMods[i].type}");
                    activeStatMods.Remove(activeStatMods[i]);
                }
            }
        }
        public int GetTotalDamageBonus()
        {
            int total = baseDamage;
            if (activeStatMods.Count > 0)
            {
                foreach (var buff in activeStatMods)
                {
                    if (buff.type == StatModType.Damage)
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