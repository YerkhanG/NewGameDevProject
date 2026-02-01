using card_system.data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace card_system.UI
{
    public class SingleCardUI : MonoBehaviour
    {
        /*[SerializeField]private CardData cardData;*/
        [SerializeField]private TextMeshProUGUI cardName;
        [SerializeField]private TextMeshProUGUI description;
        [SerializeField]private TextMeshProUGUI manaCost;
        [SerializeField]private Image image;
        [SerializeField]private CanvasGroup canvas;

        public void Setup(CardData data)
        {
            cardName.text = data.name;
            image.sprite = data.image;
        }
    }
}