using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using card_modification_system;
using card_modification_system.controller;
using card_system.data;
using combat_system;
using map_encounter_system.map_system.data.node;
using reward_system.controller;
using UnityEngine;

namespace non_battle_encounters
{
    public class EventRoomManager : MonoBehaviour
    {
        public static EventRoomManager instance;
        [SerializeField] private ModPanelController upgradePanel;
        [SerializeField]  private GameObject cardPickPanel;
        [SerializeField] private GameObject cardRewardPrefab;
        [SerializeField] private Transform canvasTransform;
        public void Awake()
        {
            DontDestroyOnLoad(this);
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private Transform GetCanvas()
        {
            if (canvasTransform == null)
            {
                var go = GameObject.FindWithTag("PopUpCanvas");
                if (go == null)
                {
                    Debug.LogError("No object tagged PopUpCanvas in the scene");
                    return null;
                }
                canvasTransform = go.transform;
            }
            return canvasTransform;
        }

        public void Open(Node node)
        {
            switch (node.eventType)
            {
                case NodeEventType.Upgrade:
                    OpenUpgradePanel(node);
                    break;
                case NodeEventType.CardPick:
                    OpenCardPickPanel(node);
                    break;
                // позже:
                // case EventType.Merchant:
                //     LoadEventScene(node);
                //     break;
            }
        }

        // private void ShowCardReward()
        // {
        //     foreach (Transform child in cardPickPanel.transform)
        //         Destroy(child.gameObject);
    
        //     cardPickPanel.SetActive(true);
            
        //     List<CardData> randomCard = CardRegistry.instance.GetRandomCardsNotOwned(3);
        //     foreach (CardData cardData in randomCard)
        //     {
        //         GameObject singleReward = Instantiate(cardRewardPrefab, cardPickPanel.transform);
        //         singleReward.GetComponent<SingleRewardController>().SetUp(cardData);   
        //     }
            
        // }
        
        private void OpenUpgradePanel(Node node)
        {
            var canvas = GetCanvas();
            if (canvas == null) return;
            var panel = Instantiate(upgradePanel, canvasTransform);
            if (!panel.TryOpenRandom())
            {
                Destroy(panel.gameObject);
                Debug.Log("No modifiable cards");
            }
        }

        private void OpenCardPickPanel(Node node)
        {
            var canvas = GetCanvas();
            if (canvas == null) return;
            var panel = Instantiate(cardPickPanel, canvasTransform);
            panel.SetActive(true);
            foreach (CardData cardData in CardRegistry.instance.GetRandomCardsNotOwned(3))
            {
                var reward = Instantiate(cardRewardPrefab, panel.transform);
                reward.GetComponent<SingleRewardController>().SetUp(cardData);
            }
        }
    }
}