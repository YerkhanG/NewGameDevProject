using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public void SetUp(Sprite image ,int level)
        {
            icon.sprite = image;
            this.level.text = level.ToString();
        }
        
    }
}