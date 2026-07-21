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
        public List<CardInstanceRecord> deck;
        [SerializeField] private List<CardData> startingDeckForTesting;
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private GameObject handContainer;
        public void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            LoadedData loadedData = PersistenceManager.instance.LoadSceneData();
            if (loadedData != null && loadedData.loadedCards != null && loadedData.loadedCards.Count > 0)
            {
                deck = loadedData.loadedCards;
            }
            else
            {
                deck = new List<CardInstanceRecord>();
                foreach (var card in startingDeckForTesting)
                {
                    deck.Add(new CardInstanceRecord
                    {
                        instanceId = Guid.NewGuid().ToString(),
                        templateId = card.id,
                        modifications = new List<CardModification>()
                    });
                }
            }
        }
        private void ShuffleDeck()
        {
            for (int i = deck.Count - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                (deck[i], deck[j]) = (deck[j], deck[i]);
            }
        }
        public void ReshuffleDeck()
        {
            List<CardInstanceRecord> shuffledCards = GraveyardPileManager.instance.GetShuffledGraveyardPile();
            deck.AddRange(shuffledCards);
        }
        // fight-end: consolidate everything into deck for saving
        public void ConsolidateAllCards()
        {
            GraveyardPileManager.instance.ShuffleFromHand();
            deck.AddRange(GraveyardPileManager.instance.graveyardPile);
            GraveyardPileManager.instance.graveyardPile.Clear();
            ShuffleDeck();
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
                var cardRec = deck[0];
                deck.RemoveAt(0);
                var instCard = Instantiate(cardPrefab, handContainer.transform);
                instCard.GetComponent<SingleCardController>().Setup(cardRec);
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