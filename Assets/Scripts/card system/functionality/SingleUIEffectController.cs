using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace card_system.functionality
{
    public class SingleUIEffectController : MonoBehaviour
    {
        public Image icon;
        public TextMeshProUGUI textArea;


        public void SetUp(Sprite icon , String text)
        {
            this.icon.sprite = icon;
            textArea.text = text;
        }
    }
}