using System;
using System.Collections.Generic;
using card_system.animation;
using card_system.data;
using card_system.UI;
using global_events;
using UnityEngine;

namespace combat_system
{
    //TODO: I would guess i need to rebind the container after scene change(maybe even other references too)
    // the issue wasnt with the references , it was with the event not unsubbing after the first instance of the fight scene 
    public class GraveyardPileManager : MonoBehaviour
    {
        [SerializeField] GameObject cardContainer;
        public static GraveyardPileManager instance;
        public List<CardData> graveyardPile =new List<CardData>();
        
        public void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            // Do NOT override cardContainer here.
            if (cardContainer == null)
                Debug.LogError("CardContainer not assigned in Inspector!");
        }
        
        private void OnEnable()
        {
            GlobalEvents.OnEndTurnButtonPressed += ShuffleFromHand;
        }
        private void OnDisable()
        {
            GlobalEvents.OnEndTurnButtonPressed -= ShuffleFromHand;
        }
        private void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }

        //after the end trun button is pressed , or the endturn is in general proced
        public void ShuffleFromHand()
        {
            if (cardContainer == null)
            {
                cardContainer = GameObject.FindWithTag("CardContainer");
                if (cardContainer == null)
                {
                    Debug.LogError("Still no card container!");
                    return;
                }
            }
            
            if (cardContainer != null)
            {
                foreach (Transform child in cardContainer.transform)
                {
                    SingleCardController card = child.gameObject.GetComponent<SingleCardController>();
                    TakeFromHand(card);
                }   
            }
        }

        public void TakeFromHand(SingleCardController card)
        {
            if (card != null)
            {
                graveyardPile.Add(card.GetCardData);
                var animationController = card.GetComponent<CardAnimationController>();
                animationController.AnimateDiscard(transform.position);
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