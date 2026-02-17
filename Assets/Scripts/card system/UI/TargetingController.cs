using model.entity;
using UnityEngine;

namespace card_system.UI
{
    public class TargetingController : MonoBehaviour
    {
        public static TargetingController instance;
        [SerializeField] private ArrowView arrowView;
        
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

        public void SetUpArrow(Vector3 startPosition)
        {
            arrowView.Show(startPosition);
        }
        public void HideArrow()
        {
            arrowView.Hide();
        }
        public Enemy ChooseTarget()
        {
            //here 
            return null;
        }
    }
}