using System;
using model.entity;
using UnityEngine;

namespace global_events
{
    public static class GlobalEvents
    {
        public static event Action<Entity> OnAttackEffectPlayed;
        public static event Action<object> OnDrawFromDeck;
        public static void RaiseAttackEffectPlayed(Entity target)
        {
            OnAttackEffectPlayed?.Invoke(target);
        }
        public static void RaiseDrawFromDeck(object card)
        {
            OnDrawFromDeck?.Invoke(card);
        }
    }
}