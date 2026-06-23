using System;
using card_system.data;
using card_system.UI;
using combat_system;
using encounter_system.data;
using encounter_system.managers;
using global_events;
using map_encounter_system.encounter_system.scene_persistance;
using reward_system.controller;
using UnityEngine;
using UnityEngine.Rendering;

namespace reward_system.managers
{
    public enum RewardType
    {
        Normal , Hard , Elite
    }
    public class RewardManager : MonoBehaviour
    {
        public GameObject cardRewardPrefab;
        public GameObject cardRewardPanel;
        
        private bool rewardShown = false;
        public void OnEnable()
        {
            GlobalEvents.OnFightWon += HandleFightWon;
            GlobalEvents.OnCardRewardPicked += HandleRewardPicked;
        }

        private void HandleFightWon()
        {
            if (rewardShown) return;
            rewardShown = true;
            switch (EncounterData.instance.currentEncounter.encounterRarity)
            {
                case Encounter.Rarity.Normal:
                    ShowCardReward();
                    break;
                case Encounter.Rarity.Hard:
                    ShowCardReward();
                    break;
                case Encounter.Rarity.Elite:
                    ShowCardReward();
                    break;
                    
            }
        }
        //the first kind of reward/For now made only for testing json saving of deck/will add gear in the future if able to 
        private void ShowCardReward()
        {
            foreach (Transform child in cardRewardPanel.transform)
                Destroy(child.gameObject);
    
            cardRewardPanel.SetActive(true);
            for (var i = 0; i < 3; i++)
            {
                CardData randomCard = CardRegistry.instance.GetRandomCardNotOwned();
                GameObject singleReward = Instantiate(cardRewardPrefab, cardRewardPanel.transform);
                singleReward.GetComponent<SingleRewardController>().SetUp(randomCard);
            }
        }

        private void HandleRewardPicked()
        {
            rewardShown = false;
            cardRewardPanel.SetActive(false);
        }
        public void OnDisable()
        {
            GlobalEvents.OnFightWon -= HandleFightWon;
            GlobalEvents.OnCardRewardPicked -= HandleRewardPicked;
        }
    }
}