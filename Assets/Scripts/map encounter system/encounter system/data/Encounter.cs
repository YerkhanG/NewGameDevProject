using System.Collections.Generic;
using UnityEngine;

namespace encounter_system.data
{
    [CreateAssetMenu(fileName = "NewEncounter", menuName = "Combat/Encounter")]
    public class Encounter : ScriptableObject
    {
        public enum Rarity
        {
            Normal,
            Hard,
            Elite
        }
        //doesnt make sense
        public List<GameObject> enemies;
        public Rarity encounterRarity;
        public Sprite encounterBackground;
    }
}