using System;
using System.Collections.Generic;
using encounter_system.data;
using model.entity;
using UnityEngine;

namespace global_events
{
    public static class GlobalEvents
    {
        public static event Action<Entity> PlayerCreated;
        public static event Action<Entity> OnAttackEffectPlayed;
        public static event Action<object> OnDrawFromDeck;
        public static event Action<Enemy> OnTargetSelected;
        public static event Action<Enemy> OnTargetValidated;
        public static event Action<int> OnManaChanged;
        public static event Action OnEndTurnButtonPressed;

        public static event Action<Encounter.Rarity> OnEncounterPicked;
        public static event Action<Encounter> OnEncounterRarityPicked;
        public static event Action<Enemy> OnEnemyDied;
        public static event Action OnFightWon;
        
        public static event Action OnCardRewardPicked;
        public static event Action<int> OnCurrencyChanged;

        public static event Action<List<Enemy>> onEncounterSpawned;
        public static void RaiseOnEncounterSpawned(List<Enemy> list) => onEncounterSpawned(list);
        public static void RaiseCurrencyChanged(int amount) => OnCurrencyChanged?.Invoke(amount);
        public static void RaiseCardRewardPicked() => OnCardRewardPicked?.Invoke();
        
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

        public static void RaiseManaChanged(int value)
        {
            OnManaChanged?.Invoke(value);
        }

        public static void RaiseEndTurnButtonPressed()
        {
            OnEndTurnButtonPressed?.Invoke();
        }

        public static void RaiseEncounterPicked(Encounter.Rarity rarity)
        {
            OnEncounterPicked?.Invoke(rarity);
        }

        public static void RaiseEncounterRarityPicked(Encounter encounter)
        {
            OnEncounterRarityPicked?.Invoke(encounter);
        }

        public static void RaiseEnemyDied(Enemy enemy)
        {
            OnEnemyDied?.Invoke(enemy);
        }

        public static void RaiseFightWon()
        {
            OnFightWon?.Invoke();
        }

        public static void RaisePlayerCreated(Entity player)
        {
            PlayerCreated?.Invoke(player);
        }
    }
}