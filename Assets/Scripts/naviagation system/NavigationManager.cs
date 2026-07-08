using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace naviagation_system
{
    public class NavigationManager : MonoBehaviour
    {
        public Button shopButton;
        public Button runButton;
        public void OnEnable()
        {
            runButton.onClick.AddListener(OnRunButtonClicked);
            shopButton.onClick.AddListener(OnShopButtonClicked);
        }

        private void OnRunButtonClicked()
        {
            try
            {
                Debug.Log("Shop button clicked");
                SceneManager.LoadScene("MapScene");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Exception in OnShopButtonClicked: " + e);
            }
        }

        public void OnDisable()
        {
            runButton.onClick.RemoveListener(OnRunButtonClicked);
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