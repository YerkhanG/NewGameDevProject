using global_events;
using metaprogression_system.managers;
using TMPro;
using UnityEngine;

namespace metaprogression_system.UI
{
    //TODO: Will have to consider issues with different scenes and etc. May be better to just use a controller
    public class SoulCoinUIController : MonoBehaviour
    {
        
        public TextMeshProUGUI soulCoinText;
        private void Start()
        {
            Debug.Log("Drawing Count : "+ SoulCoinManager.instance.GetSoulCoinCount().ToString());
            soulCoinText.text = SoulCoinManager.instance.GetSoulCoinCount().ToString();
        }
        public void OnEnable()
        {
            GlobalEvents.OnCurrencyChanged += UpdateUI;
        }

        private void UpdateUI(int amount)
        {
            Debug.Log("Updating UI on controller: "+ amount.ToString());
            soulCoinText.text = amount.ToString();
        }

        public void OnDisable()
        {
            GlobalEvents.OnCurrencyChanged -= UpdateUI;
        }
        
    }
}