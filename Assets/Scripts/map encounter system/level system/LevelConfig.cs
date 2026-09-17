using map_encounter_system.map_system;
using UnityEngine;
namespace map_encounter_system.level_system
{
    [CreateAssetMenu(fileName = "NewConfig", menuName = "Map/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public MapConfig mapConfig;
        public int floors;
        public int level;
        public Sprite icon;
    }
}