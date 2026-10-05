using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using global_events;
using map_encounter_system.level_system;
using persistence_system.manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace navigation_system
{
    public class WorldPrefabLayout : MonoBehaviour
    {
        public Image icon;
        public TextMeshProUGUI level;

        public Button selectButton;
        public LevelConfig levelConfig;
        public void SetUp(Sprite image ,int level, LevelConfig levelConfig)
        {
            icon.sprite = image;
            this.level.text = level.ToString();
            this.levelConfig = levelConfig;
        }
        public void Awake()
        {
            selectButton.onClick.AddListener(HandleSelectButtonClick);
        }
        public void HandleSelectButtonClick()
        {
            Debug.Log("Checking Button working");
            Debug.Log("Level id: " + levelConfig.Id);
            //Need to consume this somewhere , where the map scene starts 
            PersistenceManager.instance.DeleteSaveData();
            GlobalEvents.RaiseWorldSelectionStarted(levelConfig);
        }
    }
}