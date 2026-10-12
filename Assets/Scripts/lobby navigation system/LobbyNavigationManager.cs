using System;
using System.Collections.Generic;
using global_events;
using map_encounter_system.level_system;
using persistence_system.manager;
using persistence_system.model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace navigation_system
{
    public class LobbyNavigationManager : MonoBehaviour
    {
        //TODO: Need a continu button if the player quits . In that continue button i need to load the data of the level
        [Header("Panels")]
        [SerializeField] private CanvasGroup mainPanel;
        // [SerializeField] private CanvasGroup shopPanel;
        [SerializeField] private CanvasGroup levelSelectPanel;
        private MenuState menuState;
        [Header("shop button")]
        [SerializeField] private Button shopButton;
        [Header("Continue run")]
        [SerializeField] private Button continueButton;
        [Header("Level Selection")]
        [SerializeField] private Button runButton;
        [Header("Level Select Buttons")]
        [SerializeField] private Button levelSelectBackButton;
        [SerializeField] private Button[] levelButtons; 
        [Header("World Prefab")]
        [SerializeField] private GameObject worldPrefab;
        [Header("Worlds Container")]
        [SerializeField] private GameObject worldsContainer;
        [Header("Level Configs")]
        [SerializeField] private List<LevelConfig> levels;
        private LoadedData loadedData;
        public void Awake()
        {
            foreach(var l in levels)
            {
                var worldCreated = Instantiate(worldPrefab, worldsContainer.transform);
                var layout = worldCreated.GetComponent<WorldPrefabLayout>();
                layout.SetUp(l.icon,l.level,l);
            }
        }
        public void Start()
        {
            loadedData = PersistenceManager.instance.LoadSceneData();
            continueButton.interactable = !(loadedData == null);
        }
        public void OnEnable() 
        {
            runButton.onClick.AddListener(()=> SetState(MenuState.LevelSelect));
            shopButton.onClick.AddListener(OnShopButtonClicked);
            levelSelectBackButton.onClick.AddListener(() => SetState(MenuState.Main));
            continueButton.onClick.AddListener(OnContinueButtonClicked);
        }

        private void SetState(MenuState next)
        {
            menuState = next;

            Show(mainPanel,        next == MenuState.Main);
            Show(levelSelectPanel, next == MenuState.LevelSelect);
        }
        private static void Show(CanvasGroup panel, bool visible)
        {
            panel.alpha = visible ? 1f : 0f;
            panel.interactable = visible;
            panel.blocksRaycasts = visible;
        }

        public void OnDisable()
        {
            shopButton.onClick.RemoveListener(OnShopButtonClicked);
            continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        }

        public void OnShopButtonClicked()
        {
            Debug.Log("Shop button clicked");
            SceneManager.LoadScene("ShopScene");
        }

        public void OnContinueButtonClicked()
        {
            var data = PersistenceManager.instance.LoadSceneData();
            if(data.loadedCurrentFloor != null && data.loadedWorldLevelId != null)
            {
                LevelCreationManager.Instance.SetCurrentFloor(Int32.Parse(data.loadedCurrentFloor));
                LevelCreationManager.Instance.SetCurrentLevel(levels.Find(c=> c.Id == data.loadedWorldLevelId));
            }
            SceneManager.LoadScene("MapScene");
        }
    }
}