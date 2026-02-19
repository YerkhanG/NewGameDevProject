using System;
using model.entity;
using UnityEngine;

namespace global_events
{
    public static class GlobalEvents
    {
        public static event Action<Entity> OnAttackEffectPlayed;
        public static event Action<object> OnDrawFromDeck;
        public static event Action<Enemy> OnTargetSelected;
        public static event Action<Enemy> OnTargetValidated;
        public static void RaiseAttackEffectPlayed(Entity target)
        {
            OnAttackEffectPlayed?.Invoke(target);
        }
        public static void RaiseDrawFromDeck(object card)
        {
            OnDrawFromDeck?.Invoke(card);
        }
        public static void RaiseTargetSelected(Enemy target)
        {
            OnTargetSelected?.Invoke(target);
        }

        public static void RaiseTargetValidated(Enemy target)
        {
            OnTargetValidated?.Invoke(target);
        }
    }
}