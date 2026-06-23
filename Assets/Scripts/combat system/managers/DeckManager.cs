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
            
            //TODO: Check if works 
            //unoptimized bullshit
            LoadedData loadedData = PersistenceManager.instance.LoadSceneData();
            /*Map mapData = PersistenceManager.instance.LoadSceneData();*/
            if (loadedData != null && loadedData.loadedCards != null && loadedData.loadedCards.Count > 0)
            {
                deck = loadedData.loadedCards;
            }
        }
        public void ReshuffleDeck()
        {
            List<CardData> shuffledCards = GraveyardPileManager.instance.GetShuffledGraveyardPile();
            deck.AddRange(shuffledCards);
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