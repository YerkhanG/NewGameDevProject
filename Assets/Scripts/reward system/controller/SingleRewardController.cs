using System;
using card_system.data;
using combat_system;
using global_events;
using persistence_system.manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace reward_system.controller
{
    public class SingleRewardController : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private String idRefToCard;
        [SerializeField] private Button rewardButton;
        public void SetUp(CardData card)
        {
            image.sprite = card.image;
            title.text = card.cardName;
            description.text = card.description;
            idRefToCard = card.id;
        }

        public void OnEnable()
        {
            rewardButton.onClick.AddListener(HandleButtonClicked);
        }

        public void OnDisable()
        {
            rewardButton.onClick.RemoveListener(HandleButtonClicked);
        }
        protected virtual void HandleButtonClicked()
        {
            AddCardToDeck();
            GlobalEvents.RaiseCardRewardPicked();
        }

        protected void AddCardToDeck()
        {
            CardData cardToAdd = CardRegistry.instance.GetCard(idRefToCard);
            DeckManager.instance.deck.Add(cardToAdd);
            PersistenceManager.instance.SaveSceneData(cards: DeckManager.instance.deck);
        }
    }
}