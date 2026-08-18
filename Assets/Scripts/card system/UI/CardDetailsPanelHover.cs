using DG.Tweening;
using UnityEngine;

namespace card_system.UI
{
    public class CardDetailsPanelHover : MonoBehaviour
    {
        [SerializeField] private RectTransform panel;
        [SerializeField] private float appearDuration = 0.25f;

        private void OnEnable()
        {
            // сбрасываем и анимируем
            panel.DOKill();
            panel.localScale = Vector3.zero;
            panel.DOScale(Vector3.one, appearDuration)
                .SetEase(Ease.OutBack);   // пружинка: чуть перелетает и возвращается
        }
    }
}