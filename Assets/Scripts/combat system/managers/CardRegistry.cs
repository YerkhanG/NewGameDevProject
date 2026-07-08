using System;
using System.Collections.Generic;
using System.Linq;
using card_system.data;
using metaprogression_system.managers;
using NUnit.Framework;
using persistence_system.model;
using UnityEngine;
using Random = UnityEngine.Random;

namespace combat_system
{
    public class CardRegistry : MonoBehaviour
    {
        [SerializeField] List<CardData> allCards = new List<CardData>(); 
        public  static CardRegistry instance;
        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
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
            CardData[] all = Resources.LoadAll<CardData>("Cards");
            Debug.Log(all.Length);
            foreach (CardData card in all)
            {
                Debug.Log($"Card: {card.id} | Unlocked: {SessionManager.instance.IsCardUnlocked(card.id)}");
                if (SessionManager.instance.IsCardUnlocked(card.id))
                    allCards.Add(card);
            }
        }

        public void AddCard(CardData card)
        {
            allCards.Add(card);
        }

        public CardData GetCard(string id)
        {
            return allCards.Find(x => x.id == id);
        }

        public CardData GetRandomCard()
        {
            return  allCards[Random.Range(0, allCards.Count)];
        }

        public CardData GetRandomCardNotOwned()
        {
            List<CardData> notOwned = allCards.Where(c => !DeckManager.instance.deck.Contains(c)).ToList();
            if (notOwned.Count == 0) return GetRandomCard(); // fallback if player owns everything
            return notOwned[Random.Range(0, notOwned.Count)];
        }
    }
}