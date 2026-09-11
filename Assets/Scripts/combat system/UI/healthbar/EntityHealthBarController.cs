using model.entity;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace combat_system.UI
{
    public class EntityHealthBarController : MonoBehaviour
    {
        public Image healthBar;
        public Image shieldBar;
        public Image healthIcon;
        public Image damagePreviewBar;
        public Entity _entity;

        [SerializeField] private float tweenDuration = 0.3f;
        [SerializeField] private Ease tweenEase = Ease.OutQuad;

        private Tween healthTween;
        private Tween shieldTween;

        public void WireToEntity(Entity entity)
        {
            _entity = entity;
            entity.onHPChanged.AddListener(UpdateHealth);
            entity.onShieldChanged.AddListener(UpdateShield);

            // Snap to the correct value on first wire-up — no tween on entry.
            healthBar.fillAmount = entity.maxHealth > 0 ? (float)entity.currentHealth / entity.maxHealth : 0f;
            shieldBar.fillAmount = entity.maxHealth > 0 ? Mathf.Clamp01((float)entity.currentShield / entity.maxHealth) : 0f;
        }

        private void UpdateHealth(int current, int max)
        {
            float target = max > 0 ? (float)current / max : 0f;
            healthTween?.Kill();
            healthTween = healthBar.DOFillAmount(target, tweenDuration).SetEase(tweenEase);
        }

        private void UpdateShield(int shield)
        {
            float target = _entity.maxHealth > 0 ? Mathf.Clamp01((float)shield / _entity.maxHealth) : 0f;
            shieldTween?.Kill();
            shieldTween = shieldBar.DOFillAmount(target, tweenDuration).SetEase(tweenEase);
        }

        private void OnDestroy()
        {
            healthTween?.Kill();
            shieldTween?.Kill();
        }
    }
}