using System;
using System.Collections.Generic;
using card_system.functionality.card_effect_types;
using data;
using model.entity_state;
using UnityEngine;
using UnityEngine.Events;

namespace model.entity
{
    public class Entity : MonoBehaviour , IDamageable
    {
        public int currentHealth;
        public int baseDamage;
        public int currentShield;
        public int maxHealth;
        public int maxShield;
        public int armor;
        public  EntityData data;
        public List<StatMods> activeStatMods = new List<StatMods>();
        protected bool isDead = false;
        public int TotalDamage => GetTotalDamageBonus();

        public UnityEvent<int, int> onHPChanged;   // (current, max)
        public UnityEvent<int> onShieldChanged;
        public UnityEvent onDeath;
        
        [SerializeField] private float armorConstant = 50f;   // tune per game balance pass
        private const float MAX_ARMOR_REDUCTION = 0.8f; 
        protected void Awake()
        {
            IsAlive = true;
            maxHealth = data.health;
            currentHealth = maxHealth;
            baseDamage = data.baseDamage;
            maxShield = data.shield;
            armor = data.armor;
        }

        public int EffectiveArmor
        {
            get
            {
                int total = armor;
                foreach (var mod in activeStatMods)
                {
                    if (mod.type == StatModType.armor)
                        total = Mathf.RoundToInt(total + mod.amount);
                }
                return Mathf.Max(0, total);
            }
        }
        
        public float GetDamageReductionPercent()
        {
            int arm = EffectiveArmor;
            float reduction = arm / (arm + armorConstant);
            return Mathf.Min(reduction, MAX_ARMOR_REDUCTION);
        }
        
        public void ModifyBaseArmor(int amount)
        {
            armor = Mathf.Max(0, armor + amount);
            Debug.Log($"{name} permanently {(amount >= 0 ? "gained" : "lost")} {Mathf.Abs(amount)} armor (now {armor})");
        }
        public void TakeDamage(int damage)
        {
            // Armor mitigates first
            int mitigatedDamage = Mathf.RoundToInt(damage * (1f - GetDamageReductionPercent()));

            // Then shield absorbs what's left
            if (currentShield > 0)
            {
                int blocked = Mathf.Min(currentShield, mitigatedDamage);
                currentShield -= blocked;
                mitigatedDamage -= blocked;
                onShieldChanged.Invoke(currentShield);
            }

            currentHealth -= mitigatedDamage;
            onHPChanged?.Invoke(currentHealth, maxHealth);
            if (currentHealth <= 0) Die();
        }
        
        public void Heal(int amount)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
            onHPChanged.Invoke(currentHealth, maxHealth);
        }

        public void ShieldUp(int amount)
        {
            currentShield = Mathf.Clamp(currentShield + amount, 0, maxHealth);
            onShieldChanged.Invoke(currentShield);
        }
        protected virtual void Die()
        {
            if (isDead) return;
            isDead = true;
            Debug.Log(name + " died");
            IsAlive = false;
            onDeath.Invoke();
            Destroy(transform.parent.gameObject);
        }

        public bool IsAlive { get; protected set; }
        
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
            int total = baseDamage;
            if (activeStatMods.Count > 0)
            {
                foreach (var buff in activeStatMods)
                {
                    if (buff.type == StatModType.Damage)
                        total = Mathf.RoundToInt(total * buff.amount);
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