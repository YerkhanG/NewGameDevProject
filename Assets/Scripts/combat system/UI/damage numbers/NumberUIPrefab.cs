using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace combat_system.UI.damage_numbers
{
    public class NumberUIPrefab : MonoBehaviour
    {
        public Image icon;
        public TextMeshProUGUI text;
        public CanvasGroup canvasGroup;

        [Header("Animation Settings")]
        [SerializeField] private float lifetime = 1.5f;
        [SerializeField] private float floatSpeed = 50f;
        [SerializeField] private AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);
        [SerializeField] private AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 0.5f, 0.2f, 1f);
        [SerializeField] private float horizontalSpread = 0.4f;
        private float timer;
        private Vector2 startPosition;
        private RectTransform rectTransform;
        // 0 = always straight up, higher = wider fan-out
        private Vector2 direction;

        public void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            
            float randomX = UnityEngine.Random.Range(-horizontalSpread, horizontalSpread);
            direction = new Vector2(randomX, 1f).normalized;
        }
        public void Update()
        {
            timer += Time.deltaTime;
            float progress = timer / lifetime;

            Vector2 currentPos = startPosition + direction * (floatSpeed * timer);
            rectTransform.anchoredPosition = currentPos;
            if (canvasGroup)
            {
                canvasGroup.alpha = fadeCurve.Evaluate(progress);
            }
            if (progress < 0.2f)
            {
                float scale = scaleCurve.Evaluate(progress);
                rectTransform.localScale = Vector3.one * scale;
            }
            if (timer >= lifetime)
            {
                Destroy(gameObject);
            }
        }
        public void Initialize(string text, Sprite sprite, Vector2 startPos)
        {
            this.text.text = text;
            icon.sprite = sprite;
            startPosition = startPos;
            rectTransform.anchoredPosition = startPosition; // set it here too, so it doesn't wait for first Update
        }
    }
}