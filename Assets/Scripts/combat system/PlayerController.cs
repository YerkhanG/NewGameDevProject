using System;
using data;
using global_events;
using model;
using model.entity;
using UnityEngine;

namespace combat_system
{
    public class PlayerController :  MonoBehaviour
    {
        public static PlayerController instance;
        public Player player;
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void onEnable()
        {
            GlobalEvents.OnAttackEffectPlayed += AttackTarget;
        }

        private void AttackTarget(Entity target)
        {
            target.TakeDamage(player.baseDamage);
        }

        public Entity mainCharacter;

        public void RedrawCards()
        {
            Debug.Log("Draw First Card");
            DeckManager.instance.Draw();
            DeckManager.instance.Draw();
        }
    }
}