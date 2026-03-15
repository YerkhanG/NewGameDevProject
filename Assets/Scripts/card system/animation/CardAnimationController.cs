using DG.Tweening;
using UnityEngine;

namespace card_system.animation
{
    public class CardAnimationController : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private CanvasGroup canvasGroup;
        [Header("Animation Settings")]
        [SerializeField] private float drawDuration = 0.3f;
        [SerializeField] private float playDuration = 0.5f;
        [SerializeField] private float discardDuration = 0.4f;
        public void AnimateDraw(Vector3 fromPosition, System.Action onComplete = null)
        {
            // Start at deck position, small
            rectTransform.position = fromPosition;
            rectTransform.localScale = Vector3.zero;
            canvasGroup.alpha = 1f;
            
            // Pop into hand
            rectTransform.DOScale(Vector3.one, drawDuration)
                .SetEase(Ease.OutBack)
                .OnComplete(() => onComplete?.Invoke());
        }
        public void AnimateHover(bool isHovered)
        {
            if (isHovered)
            {
                rectTransform.DOScale(1.1f, 0.2f).SetEase(Ease.OutQuad);
                rectTransform.DOLocalMoveY(20f, 0.2f).SetEase(Ease.OutQuad);
            }
            else
            {
                rectTransform.DOScale(1f, 0.2f).SetEase(Ease.OutQuad);
                rectTransform.DOLocalMoveY(0f, 0.2f).SetEase(Ease.OutQuad);
            }
        }
        public void AnimatePlay(Vector3 targetPosition, System.Action onComplete = null)
        {
            // Move to target (enemy, player, etc.)
            rectTransform.DOMove(targetPosition, playDuration)
                .SetEase(Ease.InOutQuad);
            
            // Fade out and shrink
            canvasGroup.DOFade(0f, playDuration);
            rectTransform.DOScale(Vector3.zero, playDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() => {
                    onComplete?.Invoke();
                    Destroy(gameObject);
                });
        }
        public void AnimateDiscard(Vector3 graveyardPosition, System.Action onComplete = null)
        {
            // Fly to graveyard
            rectTransform.DOMove(graveyardPosition, discardDuration)
                .SetEase(Ease.InQuad);
            rectTransform.DOScale(0f, discardDuration).SetEase(Ease.OutQuad);
            canvasGroup.DOFade(0f, discardDuration)
                .OnComplete(() => {
                    onComplete?.Invoke();
                    Destroy(gameObject);
                });
        }
    }
}