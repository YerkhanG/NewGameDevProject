using System.Collections.Generic;
using card_system.functionality;
using UnityEngine;

namespace card_system.data
{
    [CreateAssetMenu(fileName = "New Card Data", menuName = "Card/Card Data")]
    public class CardData : ScriptableObject
    {
        //these will played in a loop
        [SerializeField]public List<CardEffect> cardEffects;
        [SerializeField]public TargetType targetType;
        [SerializeField]public string cardName;
        [SerializeField]public string description;
        [SerializeField]public string manaCost = "0";
        [SerializeField]public Sprite image;
    }

    public enum TargetType
    {
        ManualTargeting ,RandomEnemy, Self, SelfGear, AllEnemies
    }
}