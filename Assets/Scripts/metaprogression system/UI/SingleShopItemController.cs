using card_system.data;
using combat_system;
using metaprogression_system.managers;
using reward_system.controller;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace metaprogression_system.UI
{
    public class SingleShopItemController : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Button buyButton;
        [SerializeField] private int price;
        private CardData cardData;

        public void SetUp(CardData card, int price)
        {
            image.sprite = card.image;
            title.text = card.cardName;
            description.text = card.description;
            priceText.text = price.ToString();
            this.price = price;
            cardData = card;
        }

        private void OnEnable() => buyButton.onClick.AddListener(HandleBuyClicked);
        private void OnDisable() => buyButton.onClick.RemoveListener(HandleBuyClicked);

        private void HandleBuyClicked()
        {
            if (!SessionManager.instance.TrySpend(price))
            {
                Debug.Log("Spend Failed");
                return;
            }
            SessionManager.instance.UnlockCard(cardData.id);
            CardRegistry.instance.AddCard(cardData);
            Debug.Log("Spend Success");
            buyButton.interactable = false;
        }
    }
}