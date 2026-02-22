using System;
using global_events;
using TMPro;
using UnityEngine;
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
        [Header("Panel")]
        [SerializeField]private GameObject panel;

        public void OnEnable()
        {
            GlobalEvents.OnManaChanged += ManaChangeUI;
        }

        public void OnDisable()
        {
            GlobalEvents.OnManaChanged -= ManaChangeUI;
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
        
        private void ManaChangeUI(int manaCount)
        {
            manaCountUI.text = manaCount.ToString();
        }
    } 
}