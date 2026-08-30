using model.entity;
using UnityEngine;
using UnityEngine.UI;

namespace combat_system.UI
{
    public class EntityHealthBarController : MonoBehaviour
    {
        public Image healthBar;
        public Image shieldBar;
        public Image healthIcon;
        public Image damagePreviewBar;
        public Entity _entity;
        
        
        public void WireToEntity(Entity entity)
        {
            _entity = entity;
            // Unsubscribe previous if any, then subscribe
            entity.onHPChanged.AddListener(UpdateHealth);
            entity.onShieldChanged.AddListener(UpdateShield);
        
            // Immediate initial update
            UpdateHealth(entity.currentHealth, entity.maxHealth);
            UpdateShield(entity.currentShield);
        }
        
        
        private void UpdateHealth(int current, int max)
        {
            healthBar.fillAmount = max > 0 ? (float)current / max : 0f;
            // If you have a damage preview bar, you’d handle that here too.
        }

        private void UpdateShield(int shield)
        {
            // Show shield as an overlay on the same bar, or as a separate fill.
            // Example: use maxHP from _entity to scale shield bar width.
            float shieldRatio = _entity.maxHealth > 0 ? (float)shield / _entity.maxHealth : 0f;
            shieldBar.fillAmount = Mathf.Clamp01(shieldRatio);
        
            // Optionally show/hide shield bar or a numeric text.
        }
    }
}