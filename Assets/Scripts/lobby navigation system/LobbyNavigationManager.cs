using map_encounter_system.level_system;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace navigation_system
{
    public class LobbyNavigationManager : MonoBehaviour
    {

        [Header("Panels")]
        [SerializeField] private CanvasGroup mainPanel;
        // [SerializeField] private CanvasGroup shopPanel;
        [SerializeField] private CanvasGroup levelSelectPanel;
        private MenuState menuState;
        [Header("shop button")]
        public Button shopButton;
        [Header("Level Selection")]
        public Button runButton;
        [Header("Level Select Buttons")]
        [SerializeField] private Button levelSelectBackButton;
        [SerializeField] private Button[] levelButtons; 
        [Header("World Prefab")]
        [SerializeField] private GameObject worldPrefab;
        [Header("Worlds Container")]
        [SerializeField] private GameObject worldsContainer;
        [Header("Level Configs")]
        [SerializeField] private LevelConfig[] levels;
        public void Awake()
        {
            foreach(var l in levels)
            {
                var worldCreated = Instantiate(worldPrefab, worldsContainer.transform);
                var layout = worldCreated.GetComponent<WorldPrefabLayout>();
                layout.SetUp(l.icon,l.level);
            }
        }
        public void OnEnable()
        {
            runButton.onClick.AddListener(()=> SetState(MenuState.LevelSelect));
            shopButton.onClick.AddListener(OnShopButtonClicked);
            levelSelectBackButton.onClick.AddListener(() => SetState(MenuState.Main));
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
        }

        public void OnShopButtonClicked()
        {
            try
            {
                Debug.Log("Shop button clicked");
                SceneManager.LoadScene("ShopScene");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Exception in OnShopButtonClicked: " + e);
            }
        }
    }
}