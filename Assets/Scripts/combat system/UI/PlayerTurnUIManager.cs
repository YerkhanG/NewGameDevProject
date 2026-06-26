using System;
using card_system.data;
using card_system.UI;
using global_events;
using persistence_system.manager;
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
        [Header("Panel")]
        [SerializeField]private GameObject panel;
        [SerializeField]private GameObject deathPanel;
        [SerializeField]private GameObject victoryPanel;
        //purely for endfight card consolidation
        private bool hasConsolidated = false;
        private int fightWonCallCount = 0; 
        public void OnEnable()
        {
            GlobalEvents.OnFightWon += HandleFightWon;
            GlobalEvents.OnManaChanged += ManaChangeUI;
            endTurnButton.onClick.AddListener(EndTurnEventStart);
            deathResetButton.onClick.AddListener(OnDeathSendToMainLobby);
            afterVictoryButton.onClick.AddListener(OnVictorySendToTheMap);
        }

        private void EndTurnEventStart()
        {
            GlobalEvents.RaiseEndTurnButtonPressed();
        }

        private void OnDeathSendToMainLobby()
        {
            SceneManager.LoadScene(1);
        }

        private void OnVictorySendToTheMap()
        {
            SceneManager.LoadScene(2);
        }

        public void OnDisable()
        {
            GlobalEvents.OnFightWon -= HandleFightWon;
            GlobalEvents.OnManaChanged -= ManaChangeUI;
            endTurnButton.onClick.RemoveListener(EndTurnEventStart);
            deathResetButton.onClick.RemoveListener(OnDeathSendToMainLobby);
            afterVictoryButton.onClick.RemoveListener(OnVictorySendToTheMap);
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

        public void HandleFightWon()
        {
            if (hasConsolidated)
            {
                Debug.LogWarning("[FightWon] Already consolidated – ignoring duplicate.");
                return;
            }
            hasConsolidated = true;
            
            DeckManager.instance.ConsolidateAllCards();

            PersistenceManager.instance.SaveSceneData(cards: DeckManager.instance.deck);
            UIDeactivate();
            victoryPanel.SetActive(true);
        }
    } 
}