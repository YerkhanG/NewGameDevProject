using global_events;
using model.entity;
using UnityEngine;

namespace card_system.UI
{
    public class TargetingManager : MonoBehaviour
    {
        public static TargetingManager instance;
        [SerializeField] private ArrowView arrowView;
        public SingleCardController currentCard;
        void OnEnable()
        {
            GlobalEvents.OnTargetSelected += ChooseTarget;
        }

        void OnDisable()
        {
            GlobalEvents.OnTargetSelected -= ChooseTarget;
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
        //TODO: after i finish the turn system
        //Non manual targeting shit is here 
        /*public void */
        //Manual targeting shinenigans below 
        public void SetUpArrow(Vector3 startPosition, SingleCardController card)
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