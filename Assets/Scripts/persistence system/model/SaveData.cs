using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.U2D.Animation;

namespace persistence_system.model
{
    [Serializable]
    public class SaveData
    {
        public List<CardInstanceRecord> cards;
        public MapDTO map;
        public PlayerState playerState;
    }
}