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
        public List<StatMods> activeStatMods = new List<StatMods>();

        public int TotalDamage => GetTotalDamageBonus();
        private void Awake()
        {
            IsAlive = true;
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

        protected virtual void Die()
        {
            Debug.Log(name + " died");
            IsAlive = false;
            Destroy(transform.parent.gameObject);
        }

        public bool IsAlive { get; private set; }
        
        public void AddBuff(StatModType type, float amount, int duration)
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
            float total = baseDamage;
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