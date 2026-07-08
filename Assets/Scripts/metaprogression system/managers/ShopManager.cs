
using System.Collections.Generic;
using System.Linq;
using card_system.data;
using metaprogression_system.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace metaprogression_system.managers
{
    public class ShopManager : MonoBehaviour
    {
        public GameObject singleItemPrefab;
        public GameObject cardItemsPanel;
        public int amountOfItems = 5;
        public Button backToLobbyButton;
         private CardData[] allCards;

         public void OnEnable()
         {
             backToLobbyButton.onClick.AddListener(OnBackToLobbyButtonClicked);
         }

         public void OnDisable()
         {
             backToLobbyButton.onClick.RemoveListener(OnBackToLobbyButtonClicked);
         }

         public void OnBackToLobbyButtonClicked()
         {
             SceneManager.LoadScene("MainLobby");
         }
        public void Start()
        {
            allCards = Resources.LoadAll<CardData>("Cards");
            List<string> unlockedIds = SessionManager.instance.GetUnlockedCardIds();
        
            List<CardData> locked = new List<CardData>();
            foreach (CardData card in allCards)
            {
                if (!unlockedIds.Contains(card.id))
                    locked.Add(card);
            }

            locked = locked.OrderBy(_ => UnityEngine.Random.value).Take(amountOfItems).ToList();

            foreach (CardData card in locked)
            {
                var item = Instantiate(singleItemPrefab, cardItemsPanel.transform);
                item.GetComponent<SingleShopItemController>().SetUp(card, 200);
            }
        }
    }
}