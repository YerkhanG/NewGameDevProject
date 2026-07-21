using System;
using System.Collections.Generic;
using card_system.animation;
using card_system.data;
using card_system.UI;
using global_events;
using persistence_system.model;
using UnityEngine;

namespace combat_system
{
    //TODO: I would guess i need to rebind the container after scene change(maybe even other references too)
    // the issue wasnt with the references , it was with the event not unsubbing after the first instance of the fight scene 
    public class GraveyardPileManager : MonoBehaviour
    {
        [SerializeField] public GameObject cardContainer;
        public static GraveyardPileManager instance;
        public List<CardInstanceRecord> graveyardPile =new();
        
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
            foreach (Transform child in cardContainer.transform)
            {
                var ctrl = child.GetComponent<SingleCardController>();
                if (ctrl != null)
                {
                    graveyardPile.Add(ctrl.GetCardInstanceRecord);
                    Destroy(child.gameObject);
                }
            }
        }

        public void TakeFromHand(SingleCardController card)
        {
            if (card != null)
            {
                graveyardPile.Add(card.GetCardInstanceRecord);
                var animationController = card.GetComponent<CardAnimationController>();
                animationController.AnimateDiscard(transform.position);
            }
        }

        public List<CardInstanceRecord> GetShuffledGraveyardPile()
        {
            if (graveyardPile.Count == 0)
            {
                Debug.LogWarning("Graveyard is empty!");
                return new List<CardInstanceRecord>();
            }
            List<CardInstanceRecord> shuffledCards = new List<CardInstanceRecord>(graveyardPile);
    
            // (Fisher-Yates algorithm)
            for (int i = 0; i < shuffledCards.Count; i++)
            {
                CardInstanceRecord temp = shuffledCards[i];
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