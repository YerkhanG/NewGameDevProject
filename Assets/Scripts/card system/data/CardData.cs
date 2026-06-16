using System.Collections.Generic;
using System.Linq;
using card_system.functionality;
using UnityEngine;

namespace card_system.data
{
    [CreateAssetMenu(fileName = "New Card Data", menuName = "Card/Card Data")]
    public class CardData : ScriptableObject
    {
        public string id;
        //these will played in a loop
        [SerializeField]public List<CardEffect> cardEffects;
        [SerializeField]public string cardName;
        [SerializeField]public string description;
        [SerializeField]public string manaCost = "0";
        [SerializeField]public Sprite image;
        public bool RequiresManualTarget => cardEffects.Any(e => e.targetType == TargetType.ManualTargeting);
    }
}