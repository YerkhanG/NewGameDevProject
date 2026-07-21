using System.Collections.Generic;
using card_system.data;
using map_encounter_system.map_system.data;

namespace persistence_system.model
{
    public class LoadedData
    {
        public List<CardInstanceRecord> loadedCards;
        public Map loadedMap;
        public PlayerState playerState;
    }
}