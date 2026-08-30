using global_events;
using model.entity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace combat_system.UI.damage_numbers
{
    public class UINumberManager : MonoBehaviour
    {
        public GameObject prefab;
        public Sprite damageIcon;
        public Sprite healIcon;
        public Sprite shieldIcon;
        public Canvas Canvas;
        void OnEnable()
        {
            GlobalEvents.OnEntityDamageTaken += HandleEntityDamageTaken;
            GlobalEvents.OnEntityHealTaken += HandleEntityHealTaken;
            GlobalEvents.OnEntityShieldTaken += HandleEntityShieldTaken;
        }
        void OnDisable()
        {
            GlobalEvents.OnEntityDamageTaken -= HandleEntityDamageTaken;
            GlobalEvents.OnEntityHealTaken -= HandleEntityHealTaken;
            GlobalEvents.OnEntityShieldTaken -= HandleEntityShieldTaken;
        }

        private Vector2 canvasSpace(Entity entity)
        {
            var worldPos = entity.transform.position;
            var screenPos = Canvas.worldCamera.WorldToScreenPoint(worldPos); // same camera both ends

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                Canvas.transform as RectTransform,
                screenPos,
                Canvas.worldCamera,
                out Vector2 localPoint
            );

            return localPoint;
        }
        public void HandleEntityDamageTaken(Entity entity , int amount)
        {
            Debug.Log("Checking ui numbers 45 dmg");
            var pos =  canvasSpace(entity);
            SpawnAndShowUINumber(damageIcon, amount,pos);
        }

        public void HandleEntityHealTaken(Entity entity , int amount)
        {
            var pos =  canvasSpace(entity);
            SpawnAndShowUINumber(healIcon, amount,pos);
        }
        public void HandleEntityShieldTaken(Entity entity , int amount)
        {
            var pos =  canvasSpace(entity);
            SpawnAndShowUINumber(shieldIcon,amount,pos);
        }

        void SpawnAndShowUINumber(Sprite iconType, int amount, Vector3 position)
        {
            //changed to canvas trasnform 
            //need to remember that ui things dont show up uinless under canvas
            var uiNumber = Instantiate(prefab,Canvas.transform);
            var ui = uiNumber.GetComponent<NumberUIPrefab>();
            ui.Initialize(amount.ToString(), iconType , position );
        }
    }
}