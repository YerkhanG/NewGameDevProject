using System;
using encounter_system.data;
using map_encounter_system.map_system.data;

namespace persistence_system.model
{
    [Serializable]
    public class MapSaveData
    {
        public Map map;
        /*public Encounter.Rarity*/
        public MapSaveData(Map map)
        {
            this.map = map;
        }
    }
}