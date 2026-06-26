using System;
using System.Collections.Generic;
using card_system.animation;
using card_system.data;
using card_system.UI;
using global_events;
using persistence_system.manager;
using persistence_system.model;
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
                instance = this;
            else
                Destroy(gameObject);

            // Load saved deck immediately
            LoadedData loadedData = PersistenceManager.instance.LoadSceneData();
            if (loadedData != null && loadedData.loadedCards != null && loadedData.loadedCards.Count > 0)
            {
                deck = loadedData.loadedCards;
            }
            else
            {
                Debug.LogWarning($"[DeckManager] No saved deck – keeping inspector/empty list. Count: {deck.Count}");
            }
        }
        
        public void ReshuffleDeck()
        {
            List<CardData> shuffledCards = GraveyardPileManager.instance.GetShuffledGraveyardPile();
            deck.AddRange(shuffledCards);
        }
        // fight-end: consolidate everything into deck for saving
        public void ConsolidateAllCards()
        {
            GraveyardPileManager.instance.ShuffleFromHand();
            deck.AddRange(GraveyardPileManager.instance.graveyardPile);
            GraveyardPileManager.instance.graveyardPile.Clear();
        }
        public void TryToDrawCard()
        {
            Draw();
            Draw();
        }
        public void Draw()
        {
            if (deck.Count == 0)
                ReshuffleDeck();

            if (deck.Count > 0)
            {
                var cardData = deck[0];
                deck.RemoveAt(0);
                var instCard = Instantiate(cardPrefab, handContainer.transform);
                instCard.GetComponent<SingleCardController>().Setup(cardData);
                var animationController = instCard.GetComponent<CardAnimationController>();
                animationController.AnimateDraw(transform.position);
                GlobalEvents.RaiseDrawFromDeck(instCard);
            }
            else
            {
                Debug.LogWarning("Cannot draw - both deck and graveyard are empty!");
            }
        }
    }
}