using encounter_system.data;
using global_events;
using UnityEngine;

namespace encounter_system.managers
{
    public class EncounterSpawner : MonoBehaviour
    {
        void OnEnable()
        {
            GlobalEvents.OnEncounterRarityPicked += SpawnEncounter;
        }

        void OnDisable()
        {
            GlobalEvents.OnEncounterRarityPicked -= SpawnEncounter;
        }

        public void SpawnEncounter(Encounter encounter)
        {
            
        }
    }
}