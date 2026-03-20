using System.Collections.Generic;
using model.entity;
using UnityEngine;

namespace combat_system
{
    //this is for keeping track of all entities in the scene. At least for now.
    public class CombatEntityManager : MonoBehaviour
    {
        public List<Enemy> enemies = new List<Enemy>();
        public Entity mainCharacter;
        public static  CombatEntityManager instance;
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

        public List<Enemy> getAllEnemies()
        {
            return enemies;
        }

        public void UpdateBuffsAndDebuffsEnemies()
        {
            
        }

        public void UpdateBuffsAndDebuffsMC()
        {
            mainCharacter.UpdateBuffs();
        }
    }
}