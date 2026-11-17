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
        //Also need event encounters now
        //TODO: need to change for both battle and event
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
        
        public Encounter PickRandomBattleEncounterByRarity(Encounter.Rarity rarity)
        {
            var pickedEncounters = encounters.Where(encounter => encounter.encounterRarity == rarity).ToList();
            /*GlobalEvents.RaiseEncounterRarityPicked(pickedEncounters[Random.Range(0, pickedEncounters.Count)]);*/
            return  pickedEncounters[Random.Range(0, pickedEncounters.Count)];
        }

        public Encounter FindById(string id)
        {
            return encounters.Find(e => e.encounterId == id);
        }
        
    }
}