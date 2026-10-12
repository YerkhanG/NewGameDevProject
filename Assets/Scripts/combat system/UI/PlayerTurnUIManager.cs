using System;
using System.Linq;
using card_modification_system.controller;
using card_system.data;
using card_system.UI;
using global_events;
using model.entity;
using navigation_system;
using persistence_system.manager;
using persistence_system.model;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace combat_system.UI
{
    //This is for the general control of the player UI(activating it and shit)
    public class PlayerTurnUIManager : MonoBehaviour
    {
        public static PlayerTurnUIManager instance;
        [Header("Buttons and Counters")] 
        [SerializeField]private TextMeshProUGUI manaCountUI;
        [SerializeField]private Button endTurnButton;
        [SerializeField]private Button deathResetButton;
        [SerializeField]private Button afterVictoryButton;
        [SerializeField]private Button NextFloorButton; 
        [SerializeField]private Button FinishFloorButton;
        [Header("Panel")]
        [SerializeField]private GameObject panel;
        [SerializeField]private GameObject deathPanel;
        [SerializeField]private GameObject victoryPanel;
        [SerializeField]private GameObject nextFloorPanel;
        //TODO: Maybe the panel will be replaced once i change the level done event chain
        [SerializeField]private GameObject levelFinishedPanel;

        //--------------------
        [SerializeField]private GameObject cardModPanel;


        //purely for endfight card consolidation
        private bool hasConsolidated = false;
        public void OnEnable()
        {
            GlobalEvents.OnFightWon += HandleFightWon;
            GlobalEvents.OnManaChanged += ManaChangeUI;
            GlobalEvents.OnLevelCleared += HandleLevelWon;
            endTurnButton.onClick.AddListener(EndTurnEventStart);
            deathResetButton.onClick.AddListener(OnDeathSendToMainLobby);
            afterVictoryButton.onClick.AddListener(OnVictorySendToTheMap);
            NextFloorButton.onClick.AddListener(OnFloorWonSendToTheNextMap);
        }

        private void EndTurnEventStart()
        {
            GlobalEvents.RaiseEndTurnButtonPressed();
        }

        private void OnDeathSendToMainLobby()
        {
            SceneManager.LoadScene("MainLobby");
        }
        //in theory this button is used after every reward and shebang that could be done after winning , so using it could be good , idk. 
        private void OnVictorySendToTheMap()
        {
            SceneManager.LoadScene("MapScene");
        }
        private void OnFloorWonSendToTheNextMap()
        {
            SceneManager.LoadScene("MapScene");
        }
        public void OnDisable()
        {
            GlobalEvents.OnFightWon -= HandleFightWon;
            GlobalEvents.OnManaChanged -= ManaChangeUI;
            GlobalEvents.OnLevelCleared -= HandleLevelWon;
            endTurnButton.onClick.RemoveListener(EndTurnEventStart);
            deathResetButton.onClick.RemoveListener(OnDeathSendToMainLobby);
            afterVictoryButton.onClick.RemoveListener(OnVictorySendToTheMap);
            NextFloorButton.onClick.RemoveListener(OnFloorWonSendToTheNextMap);
        }
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

        public void Start()
        {
            manaCountUI.text = ManaCountManager.instance.GetManaCount().ToString();
        }

        public void UIDeactivate()
        {
            Debug.Log("UIDeactivate called - setting panel to false");
            panel.SetActive(false);
        }

        public void UIActivate()
        {
            Debug.Log("UIActivate called - setting panel to true");
            /*endTurnButton.enabled = true;
            manaCountUI.enabled = true;*/
            panel.SetActive(true);
        }

        public void DeathScreen()
        {
            UIDeactivate();
            deathPanel.SetActive(true);
        }
        private void ManaChangeUI(int manaCount)
        {
            manaCountUI.text = manaCount.ToString();
        }
        public void HandleLevelWon()
        {
            UIDeactivate();
            levelFinishedPanel.SetActive(true);
        }
        public void HandleFightWon()
        {
            if (hasConsolidated)
            {
                Debug.LogWarning("[FightWon] Already consolidated – ignoring duplicate.");
                return;
            }
            hasConsolidated = true;
            
            DeckManager.instance.ConsolidateAllCards();
            
            Player player = (Player)CombatEntityManager.instance.mainCharacter;
            PlayerState playerState = new PlayerState
            {
                health = player.currentHealth,
                maxHealth = player.maxHealth,
                baseDamage = player.baseDamage,
            };  
            cardModPanel.SetActive(true);
            var card = PickCardsWithLimit();
            if (card != null)
            {
                cardModPanel.SetActive(true);
                cardModPanel.GetComponent<ModPanelController>().Open(card);
            }
            PersistenceManager.instance.SaveSceneData(cards: DeckManager.instance.deck, playerState: playerState);
            UIDeactivate();
            if (LevelCreationManager.Instance.EnteredFinalNode)
            {   
                LevelCreationManager.Instance.HandleWinningFloor();
                nextFloorPanel.SetActive(true);
            }
            else
            {
                victoryPanel.SetActive(true);
            }
        }

        public CardInstanceRecord PickCardsWithLimit()
        {
            var modPanelController = cardModPanel.GetComponent<ModPanelController>();

            var eligible = DeckManager.instance.deck
                .Where(c => modPanelController.HasAvailableChoices(c))
                .ToList();

            if (eligible.Count == 0) return null;

            return eligible[UnityEngine.Random.Range(0, eligible.Count)];
        }
    } 
}