using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.U2D.Animation;

namespace persistence_system.model
{
    //I will have to save the unclocked stuff here , and unload somewhere else
    [Serializable]
    public class SaveData
    {
        public List<CardDTO> cards;
        public MapDTO map;
        public PlayerState playerState;
    }
}