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
        //this is  unclockables , I think . Probably a list of all cards and chars currently in posession ,
        //i can just save all of em here. Some randomized mechanics will probably use all of these in a different manager
        public List<CardData> cards = new List<CardData>();
        public List<CharData> characters = new List<CharData>();
        public PlayerState playerState;
    }
}