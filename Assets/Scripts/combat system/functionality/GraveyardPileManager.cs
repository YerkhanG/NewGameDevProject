using System;
using System.Collections.Generic;
using card_system.data;
using card_system.UI;
using global_events;
using UnityEngine;

namespace combat_system
{
    // TODO: 2# create a pile where played and unplayed cards at the end of the turn get shuffled.
    // And then after the deck is empty it gets shuffled back into the deck.
    public class GraveyardPileManager : MonoBehaviour
    {
        [SerializeField] GameObject cardContainer;
        public static GraveyardPileManager instance;
        private List<CardData> graveyardPile =new List<CardData>();
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

        private void OnEnable()
        {
            GlobalEvents.OnEndTurnButtonPressed += TakeFromHand;
        }

        //after the end trun button is pressed , or the endturn is in general proced
        private void TakeFromHand()
        {
            foreach (Transform child in cardContainer.transform)
            {
                SingleCardController card = child.gameObject.GetComponent<SingleCardController>();
                if (card != null)
                {
                    CardData cardData = card.GetCardData;
                    graveyardPile.Add(cardData);
                    Destroy(card.gameObject);
                }
            }
        }

        public List<CardData> GetShuffledGraveyardPile()
        {
            if (graveyardPile.Count == 0)
            {
                Debug.LogWarning("Graveyard is empty!");
                return new List<CardData>();
            }
            List<CardData> shuffledCards = new List<CardData>(graveyardPile);
    
            // (Fisher-Yates algorithm)
            for (int i = 0; i < shuffledCards.Count; i++)
            {
                CardData temp = shuffledCards[i];
                int randomIndex = UnityEngine.Random.Range(i, shuffledCards.Count);
                shuffledCards[i] = shuffledCards[randomIndex];
                shuffledCards[randomIndex] = temp;
            }
    
            Debug.Log($"Shuffling {shuffledCards.Count} cards from graveyard back to deck");
    
            graveyardPile.Clear();
    
            return shuffledCards;
        }
    }
}