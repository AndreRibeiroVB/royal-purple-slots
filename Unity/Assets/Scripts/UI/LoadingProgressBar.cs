// LoadingProgressBar.cs
// Controla a animação da barra de progresso de loading com efeitos visuais
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace Slots.UI
{
    public class LoadingProgressBar : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Image fillImage;
        [SerializeField] private TextMeshProUGUI percentageText;
        [SerializeField] private Image glowEffect;

        [Header("Animation Settings")]
        [SerializeField] private AnimationCurve progressCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        [SerializeField] private float glowPulseSpeed = 2f;
        [SerializeField] private float glowMinAlpha = 0.3f;
        [SerializeField] private float glowMaxAlpha = 0.8f;

        [Header("Colors")]
        [SerializeField] private Color startColor = new Color(0.5f, 0.3f, 0.8f); // Purple
        [SerializeField] private Color endColor = new Color(1f, 0.84f, 0f); // Gold

        private Coroutine glowCoroutine;

        private void OnEnable()
        {
            if (fillImage)
            {
                fillImage.fillAmount = 0f;
            }

            if (percentageText)
            {
                percentageText.text = "0%";
            }

            if (glowEffect)
            {
                glowCoroutine = StartCoroutine(AnimateGlow());
            }
        }

        private void OnDisable()
        {
            if (glowCoroutine != null)
            {
                StopCoroutine(glowCoroutine);
            }
        }

        public IEnumerator AnimateProgress(float duration)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                float curveValue = progressCurve.Evaluate(t);

                // Update fill amount
                if (fillImage)
                {
                    fillImage.fillAmount = curveValue;
                    // Gradient color from purple to gold
                    fillImage.color = Color.Lerp(startColor, endColor, curveValue);
                }

                // Update percentage text
                if (percentageText)
                {
                    int percentage = Mathf.RoundToInt(curveValue * 100);
                    percentageText.text = $"{percentage}%";
                }

                yield return null;
            }

            // Ensure we reach 100%
            if (fillImage)
            {
                fillImage.fillAmount = 1f;
                fillImage.color = endColor;
            }

            if (percentageText)
            {
                percentageText.text = "100%";
            }
        }

        private IEnumerator AnimateGlow()
        {
            if (!glowEffect) yield break;

            Color glowColor = glowEffect.color;

            while (true)
            {
                float alpha = Mathf.Lerp(glowMinAlpha, glowMaxAlpha, 
                    (Mathf.Sin(Time.time * glowPulseSpeed) + 1f) / 2f);
                glowColor.a = alpha;
                glowEffect.color = glowColor;

                yield return null;
            }
        }

        // Public method to set progress manually (for actual asset loading)
        public void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);

            if (fillImage)
            {
                fillImage.fillAmount = progress;
                fillImage.color = Color.Lerp(startColor, endColor, progress);
            }

            if (percentageText)
            {
                int percentage = Mathf.RoundToInt(progress * 100);
                percentageText.text = $"{percentage}%";
            }
        }
    }
}
