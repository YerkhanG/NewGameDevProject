using System;
using System.Collections.Generic;
using card_system.data;
using card_system.UI;
using global_events;
using UnityEngine;

namespace combat_system
{
    public class DeckManager : MonoBehaviour
    {
        public static  DeckManager instance;
        public List<CardData> deck;
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private GameObject handContainer;
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

        public void Draw()
        {
            if (deck[0] != null)
            {
                var cardData =  deck[0];
                deck.RemoveAt(0);
                var instCard = Instantiate(cardPrefab, cardPrefab.transform.position, cardPrefab.transform.rotation);
                instCard.GetComponent<SingleCardUI>().Setup(cardData);
                instCard.transform.SetParent(handContainer.transform);
                GlobalEvents.RaiseDrawFromDeck(instCard);
            }
        }
    }
}