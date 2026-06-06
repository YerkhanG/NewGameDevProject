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
        public List<CardData> cards = new List<CardData>();
        public List<CharData> characters = new List<CharData>();
    }
}