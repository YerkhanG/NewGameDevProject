using System;
using model;
using model.entity;
using UnityEngine;

namespace combat_system
{
    public class PlayerController :  MonoBehaviour
    {
        public static PlayerController instance;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public Entity mainCharacter;

        public void RedrawCards()
        {
            Debug.Log("Draw First Card");
            DeckManager.instance.Draw();
        }
    }
}