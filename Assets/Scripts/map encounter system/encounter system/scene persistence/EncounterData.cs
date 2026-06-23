using encounter_system.data;
using UnityEngine;

namespace map_encounter_system.encounter_system.scene_persistance
{
    public class EncounterData : MonoBehaviour
    {
        public static EncounterData instance;
        public Encounter currentEncounter;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); return; }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}