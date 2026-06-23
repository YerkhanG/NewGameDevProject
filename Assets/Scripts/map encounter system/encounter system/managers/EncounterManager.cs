using System;
using System.Collections.Generic;
using System.Linq;
using encounter_system.data;
using global_events;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace encounter_system.managers
{
    public class EncounterManager : MonoBehaviour
    {
        public List<Encounter>  encounters;
        public static EncounterManager instance;
        public void Awake()
        {
            DontDestroyOnLoad(this);
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public Encounter PickRandomEncounterByRarity(Encounter.Rarity rarity)
        {
            var pickedEncounters = encounters.Where(encounter => encounter.encounterRarity == rarity).ToList();
            /*GlobalEvents.RaiseEncounterRarityPicked(pickedEncounters[Random.Range(0, pickedEncounters.Count)]);*/
            return  pickedEncounters[Random.Range(0, pickedEncounters.Count)];
        }
        
    }
}