
using encounter_system.data;
using global_events;
using map_encounter_system.encounter_system.scene_persistance;
using map_encounter_system.map_system.data.node;
using non_battle_encounters;
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
        private void HandleEncounterPicked(Node node)
        {
            switch (node.nodeCategory)
            {
                case NodeCategory.Battle:
                    EncounterData.instance.currentEncounter = node.encounter;
                    SceneManager.LoadScene("FightScene1");
                    break;
                case NodeCategory.Event:
                    EventRoomManager.instance.Open(node);
                    break;
            }
        }

        void OnDisable()
        {
            GlobalEvents.OnEncounterPicked -= HandleEncounterPicked;
        }
    }
}