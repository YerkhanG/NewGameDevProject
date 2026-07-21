using System.Collections.Generic;
using System.Linq;
using card_system.data;
using card_system.functionality;
using metaprogression_system.managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace combat_system
{
    //Need to use CardData here as Cardregistry hold ref to template cards, not to specific instances of em.
    public class CardRegistry : MonoBehaviour
    {
        [SerializeField] List<CardData> allCards = new List<CardData>();
        [SerializeField] List<CardEffect> allEffects = new List<CardEffect>();
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

        public void Start() => PopulateRegistry();
        private void PopulateRegistry()
        {
            if (SessionManager.instance == null)
            {
                Debug.LogWarning("[CardRegistry] SessionManager not ready yet — can't populate.");
                return;
            }
            CardData[] all = Resources.LoadAll<CardData>("Cards");
            foreach (CardData card in all)
            {
                bool unlocked = SessionManager.instance.IsCardUnlocked(card.id);
                if (unlocked)
                    allCards.Add(card);
            }
        }
        public void AddCard(CardData card)
        {
            if (!allCards.Any(c => c.id == card.id))
                allCards.Add(card);
        }

        public CardData GetCard(string id)
        {
            if (allCards.Count == 0) PopulateRegistry();
            return allCards.Find(x => x.id == id);
        }

        public CardEffect GetCardEffect(string id)
        {
            return allEffects.Find(x => x.id == id);
        }
        /*public CardData GetRandomCard()
        {
            return  allCards[Random.Range(0, allCards.Count)];
        }*/

        public List<CardData> GetRandomCardsNotOwned(int count)
        {
            List<CardData> notOwned = allCards
                .Where(c => DeckManager.instance.deck.All(rec => rec.templateId != c.id))
                .ToList();

            Shuffle(notOwned);

            if (notOwned.Count >= count)
                return notOwned.Take(count).ToList();

            // Not enough unowned — top up from the full pool without repeating what's already chosen
            List<CardData> result = new List<CardData>(notOwned);
            List<CardData> fallback = allCards.Except(result).ToList();
            Shuffle(fallback);
            result.AddRange(fallback.Take(count - result.Count));

            // Last resort only if allCards itself has fewer than `count` cards total
            while (result.Count < count && allCards.Count > 0)
                result.Add(allCards[Random.Range(0, allCards.Count)]);

            return result;
        }

        private void Shuffle(List<CardData> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}