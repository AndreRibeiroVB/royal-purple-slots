// SymbolAnimator.cs
// Animações de símbolos: glow, pulsar, shake, etc.
using System.Collections;
using UnityEngine;

namespace Slots.Anim
{
    public class SymbolAnimator : MonoBehaviour
    {
        [Header("Glow Settings")]
        public Color glowColor = new Color(1f, 0.9f, 0.3f, 1f);
        public float glowIntensity = 2f;
        public float glowPulseSpeed = 2f;

        [Header("Animation Settings")]
        public float bounceHeight = 0.3f;
        public float bounceSpeed = 3f;
        public float scaleAmount = 1.2f;
        public float rotationAmount = 10f;

        private SpriteRenderer spriteRenderer;
        private Vector3 originalScale;
        private Vector3 originalPosition;
        private Material glowMaterial;
        private bool isAnimating = false;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            originalScale = transform.localScale;
            originalPosition = transform.localPosition;
            
            // Criar material com shader de glow
            if (spriteRenderer != null && spriteRenderer.material != null)
            {
                glowMaterial = new Material(spriteRenderer.material);
            }
        }

        /// <summary>
        /// Inicia animação de glow no símbolo vencedor
        /// </summary>
        public void StartWinGlow(float duration = 2f)
        {
            if (isAnimating) return;
            StartCoroutine(GlowAnimation(duration));
        }

        private IEnumerator GlowAnimation(float duration)
        {
            isAnimating = true;
            float elapsed = 0f;

            if (spriteRenderer != null && glowMaterial != null)
            {
                spriteRenderer.material = glowMaterial;
            }

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                
                // Pulsar o brilho
                float intensity = Mathf.Lerp(1f, glowIntensity, 
                    (Mathf.Sin(elapsed * glowPulseSpeed * Mathf.PI) + 1f) * 0.5f);
                
                if (spriteRenderer != null)
                {
                    Color glowCol = glowColor * intensity;
                    spriteRenderer.color = glowCol;
                }

                yield return null;
            }

            // Resetar cor
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.white;
            }

            isAnimating = false;
        }

        /// <summary>
        /// Animação de bounce para destaque
        /// </summary>
        public void PlayBounceAnimation()
        {
            if (isAnimating) return;
            StartCoroutine(BounceAnimation());
        }

        private IEnumerator BounceAnimation()
        {
            isAnimating = true;
            float elapsed = 0f;
            float duration = 0.6f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;

                // Bounce usando sin wave
                float bounce = Mathf.Sin(t * Mathf.PI * bounceSpeed) * bounceHeight;
                transform.localPosition = originalPosition + Vector3.up * bounce;

                yield return null;
            }

            transform.localPosition = originalPosition;
            isAnimating = false;
        }

        /// <summary>
        /// Animação de escala (grow and shrink)
        /// </summary>
        public void PlayPulseAnimation(float duration = 1f)
        {
            if (isAnimating) return;
            StartCoroutine(PulseAnimation(duration));
        }

        private IEnumerator PulseAnimation(float duration)
        {
            isAnimating = true;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = (Mathf.Sin(elapsed * Mathf.PI * 2f) + 1f) * 0.5f;

                Vector3 targetScale = originalScale * Mathf.Lerp(1f, scaleAmount, t);
                transform.localScale = targetScale;

                yield return null;
            }

            transform.localScale = originalScale;
            isAnimating = false;
        }

        /// <summary>
        /// Animação de shake/tremer
        /// </summary>
        public void PlayShakeAnimation(float intensity = 0.1f, float duration = 0.3f)
        {
            if (isAnimating) return;
            StartCoroutine(ShakeAnimation(intensity, duration));
        }

        private IEnumerator ShakeAnimation(float intensity, float duration)
        {
            isAnimating = true;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                
                Vector3 randomOffset = new Vector3(
                    Random.Range(-intensity, intensity),
                    Random.Range(-intensity, intensity),
                    0f
                );

                transform.localPosition = originalPosition + randomOffset;

                yield return null;
            }

            transform.localPosition = originalPosition;
            isAnimating = false;
        }

        /// <summary>
        /// Animação de rotação
        /// </summary>
        public void PlayRotateAnimation(float duration = 1f)
        {
            if (isAnimating) return;
            StartCoroutine(RotateAnimation(duration));
        }

        private IEnumerator RotateAnimation(float duration)
        {
            isAnimating = true;
            float elapsed = 0f;
            Quaternion originalRotation = transform.localRotation;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = (Mathf.Sin(elapsed * Mathf.PI * 2f)) * rotationAmount;

                transform.localRotation = originalRotation * Quaternion.Euler(0f, 0f, t);

                yield return null;
            }

            transform.localRotation = originalRotation;
            isAnimating = false;
        }

        /// <summary>
        /// Para todas as animações e reseta o símbolo
        /// </summary>
        public void StopAllAnimations()
        {
            StopAllCoroutines();
            transform.localScale = originalScale;
            transform.localPosition = originalPosition;
            transform.localRotation = Quaternion.identity;
            
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.white;
            }

            isAnimating = false;
        }

        /// <summary>
        /// Animação combinada para Big Win
        /// </summary>
        public void PlayBigWinAnimation()
        {
            StartCoroutine(BigWinComboAnimation());
        }

        private IEnumerator BigWinComboAnimation()
        {
            float duration = 2f;
            
            // Simultaneamente: glow + pulse + rotate
            StartCoroutine(GlowAnimation(duration));
            StartCoroutine(PulseAnimation(duration));
            StartCoroutine(RotateAnimation(duration));

            yield return new WaitForSeconds(duration);
        }
    }
}
