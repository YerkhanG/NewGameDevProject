using System;
using System.Collections.Generic;
using card_system.functionality;
using encounter_system.data;
using map_encounter_system.level_system;
using model.entity;

namespace global_events
{
    /// <summary>
    /// Central static event hub for global game events.
    /// </summary>
    public static class GlobalEvents
    {
        #region Player, Turn, Combat, and Reward Events

        // Turn events
        public static event Action OnPlayerTurnEnded;
        public static event Action OnPlayerTurnStarted;

        // Player / entity events
        public static event Action<Entity> PlayerCreated;
        public static event Action<Entity> OnAttackEffectPlayed;
        public static event Action<object> OnDrawFromDeck;
        public static event Action<Enemy> OnTargetSelected;
        public static event Action<Enemy> OnTargetValidated;
        public static event Action<int> OnManaChanged;
        public static event Action OnEndTurnButtonPressed;
        public static event Action<Enemy> OnEnemyDied;
        public static event Action OnFightWon;
        public static event Action OnCardRewardPicked;
        public static event Action<int> OnCurrencyChanged;

        public static void RaisePlayerTurnEnded()
        {
            OnPlayerTurnEnded?.Invoke();
        }

        public static void RaisePlayerTurnStarted()
        {
            OnPlayerTurnStarted?.Invoke();
        }

        public static void RaisePlayerCreated(Entity player)
        {
            PlayerCreated?.Invoke(player);
        }

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

        public static void RaiseEnemyDied(Enemy enemy)
        {
            OnEnemyDied?.Invoke(enemy);
        }

        public static void RaiseFightWon()
        {
            OnFightWon?.Invoke();
        }

        public static void RaiseCardRewardPicked() => OnCardRewardPicked?.Invoke();

        public static void RaiseCurrencyChanged(int amount) => OnCurrencyChanged?.Invoke(amount);

        #endregion

        #region Encounter System Events

        public static event Action<Encounter.Rarity> OnEncounterPicked;
        public static event Action<Encounter> OnEncounterRarityPicked;
        public static event Action<List<Enemy>> onEncounterSpawned;

        public static void RaiseEncounterPicked(Encounter.Rarity rarity)
        {
            OnEncounterPicked?.Invoke(rarity);
        }

        public static void RaiseEncounterRarityPicked(Encounter encounter)
        {
            OnEncounterRarityPicked?.Invoke(encounter);
        }

        public static void RaiseOnEncounterSpawned(List<Enemy> list) => onEncounterSpawned(list);

        #endregion

        #region Card Details / Hover Events

        public static event Action<List<CardEffect>> onMouseCardHoverStart;
        public static event Action<List<CardEffect>> onMouseCardHoverEnd;

        public static void RaiseMouseCardHoverStart(List<CardEffect> effects)
        {
            onMouseCardHoverStart?.Invoke(effects);
        }

        public static void RaiseMouseCardHoverEnd(List<CardEffect> effects)
        {
            onMouseCardHoverEnd?.Invoke(effects);
        }

        #endregion

        #region UI Number Events

        public static Action<Entity, int> OnEntityDamageTaken;
        public static Action<Entity, int> OnEntityHealTaken;
        public static Action<Entity, int> OnEntityShieldTaken;

        public static void RaiseEntityDamageTaken(Entity entity, int amount) => OnEntityDamageTaken?.Invoke(entity, amount);
        public static void RaiseEntityHealTaken(Entity entity, int amount) => OnEntityHealTaken?.Invoke(entity, amount);
        public static void RaiseEntityShieldTaken(Entity entity, int amount) => OnEntityShieldTaken?.Invoke(entity, amount);

        #endregion

        #region World Selection / Level / Floor Events

        public static Action<LevelConfig> OnWorldSelected;
        public static Action OnLevelCleared;

        public static Action OnFloorFinished;
        public static Action OnContinueRunClicked;

        public static void RaiseWorldSelectionStarted(LevelConfig levelConfig) => OnWorldSelected?.Invoke(levelConfig);
        public static void RaiseLevelCleared() => OnLevelCleared?.Invoke();

        public static void RaiseFloorFinished() => OnFloorFinished?.Invoke();
        public static void RaiseContinueButtonClicked() => OnContinueRunClicked?.Invoke();

        #endregion
    }
}