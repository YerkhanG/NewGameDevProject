using combat_system;
using encounter_system.data;
using global_events;
using map_encounter_system.encounter_system.scene_persistance;
using model.entity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace encounter_system.managers
{
    public class EncounterSpawner : MonoBehaviour
    {
        void OnEnable()
        {
            /*DontDestroyOnLoad(this);*/
            GlobalEvents.OnEncounterPicked += HandleEncounterPicked;
        }

        private void HandleEncounterPicked(Encounter.Rarity rarity)
        {
            Encounter pickedEnc = EncounterManager.instance.PickRandomEncounterByRarity(rarity);
            EncounterData.instance.currentEncounter = pickedEnc; // store it
            SceneManager.LoadScene("FightScene1");
        }

        void OnDisable()
        {
            GlobalEvents.OnEncounterPicked -= HandleEncounterPicked;
        }
    }
}