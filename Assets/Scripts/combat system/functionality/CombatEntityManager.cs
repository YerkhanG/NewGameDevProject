using UnityEngine;

namespace combat_system
{
    //this is for keeping track of all entities in the scene. At least for now.
    public class CombatEntityManager : MonoBehaviour
    {
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
    }
}