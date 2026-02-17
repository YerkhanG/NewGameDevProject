using System.Collections.Generic;
using card_system.functionality;
using UnityEngine;

namespace card_system.data
{
    [CreateAssetMenu(fileName = "New Card Data", menuName = "Card/Card Data")]
    public class CardData : ScriptableObject
    {
        //these will played in a loop
        [SerializeField]private List<CardEffect> cardEffects;  
        [SerializeField]public string cardName;
        [SerializeField]public string description;
        [SerializeField]public string manaCost;
        [SerializeField]public Sprite image;
        [SerializeField] public bool manualTargeting;

    }
}