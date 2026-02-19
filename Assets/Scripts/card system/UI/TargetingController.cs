using global_events;
using model.entity;
using UnityEngine;

namespace card_system.UI
{
    public class TargetingController : MonoBehaviour
    {
        public static TargetingController instance;
        [SerializeField] private ArrowView arrowView;
        public SingleCardUI currentCard;
        void OnEnable()
        {
            GlobalEvents.OnTargetSelected += ChooseTarget;
        }
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

        public void SetUpArrow(Vector3 startPosition, SingleCardUI card)
        {
            currentCard = card;
            arrowView.Show(startPosition);
        }
        public void HideArrow()
        {
            arrowView.Hide();
            currentCard = null;
        }
        public void ChooseTarget(Enemy enemy)
        {
            arrowView.Hide();
            if (currentCard)
            {
                //activate card here 
                currentCard.PlayCard(enemy);
                currentCard = null;
            }   
        }
    }
}