using System;
using System.Collections.Generic;
using card_system.data;
using card_system.UI;
using global_events;
using UnityEngine;

namespace combat_system
{
    //TODO: 3# here extend the class so it can get cards from the graveyardpile(name still unsure)
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
        //after deck is empty reshuffle cards from the graveyard 
        public void ReshuffleDeck()
        {
            List<CardData> shuffledCards = GraveyardPileManager.instance.GetShuffledGraveyardPile();
            deck = shuffledCards;
        }

        public void TryToDrawCard()
        {
            Draw();
            Draw();
        }
        public void Draw()
        {
            if (deck.Count == 0)
            {
                ReshuffleDeck();
            }
            if (deck.Count > 0)
            {
                var cardData = deck[0];
                deck.RemoveAt(0);
                var instCard = Instantiate(cardPrefab, handContainer.transform);
                instCard.GetComponent<SingleCardController>().Setup(cardData);
                GlobalEvents.RaiseDrawFromDeck(instCard);
            }
            else
            {
                Debug.LogWarning("Cannot draw - both deck and graveyard are empty!");
            }
        }
    }
}