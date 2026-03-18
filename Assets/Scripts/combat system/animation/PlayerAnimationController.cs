using DG.Tweening;
using global_events;
using model.entity;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

namespace combat_system.animation
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private float moveDuration = 0.3f;
        private float offset = 3.5f;
        private Tweener _moveTween;
        private Sequence _attackSequence;
        void OnEnable()
        {
            GlobalEvents.OnAttackEffectPlayed += AnimateAttack;
        }

        void OnDisable()
        {
            GlobalEvents.OnAttackEffectPlayed -= AnimateAttack;
            KillTweens();
        }
        public void AnimateAttack(Entity target)
        {
            Vector3 originalPos = transform.position;
            Vector3 targetPos = target.transform.position;
            
            Debug.Log("attacking target " + target.name);
            _attackSequence = DOTween.Sequence();
            _attackSequence.Append(transform.parent.DOMove(targetPos, moveDuration).SetEase(Ease.OutBack));
            _attackSequence.Append(transform.parent.DOMove(originalPos, moveDuration).SetEase(Ease.OutQuad));
            
            // Optional: add a callback when the whole sequence finishes
            _attackSequence.OnComplete(() => Debug.Log("Attack animation complete"));
        }
        
        private void KillTweens()
        {
            if (_attackSequence != null && _attackSequence.IsActive())
                _attackSequence.Kill();
            if (_moveTween != null && _moveTween.IsActive())
                _moveTween.Kill();
        }
    }
}