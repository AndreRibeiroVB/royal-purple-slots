// ReelAnimator.cs
// Animações de rolos com easing suave
using System.Collections;
using UnityEngine;

namespace Slots.Anim
{
    public class ReelAnimator : MonoBehaviour
    {
        [Header("Spin Settings")]
        public float spinDuration = 2f;
        public float anticipationDelay = 0.2f;
        public AnimationCurve spinCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        
        [Header("Visual Settings")]
        public float spinSpeed = 10f;
        public float blurAmount = 1f;
        public GameObject blurEffect;

        [Header("Stop Settings")]
        public AnimationCurve stopCurve = AnimationCurve.EaseOut(0, 1, 1, 0);
        public float bounceAmount = 0.2f;
        public float bounceDecay = 0.5f;

        private Vector3 originalPosition;
        private bool isSpinning = false;
        private float currentSpeed = 0f;

        private void Awake()
        {
            originalPosition = transform.localPosition;
        }

        /// <summary>
        /// Inicia a animação de spin com delay opcional
        /// </summary>
        public void StartSpin(float delayBeforeStart = 0f)
        {
            if (isSpinning) return;
            StartCoroutine(SpinAnimation(delayBeforeStart));
        }

        private IEnumerator SpinAnimation(float delay)
        {
            if (delay > 0f)
            {
                // Animação de antecipação
                yield return StartCoroutine(AnticipationAnimation());
                yield return new WaitForSeconds(delay);
            }

            isSpinning = true;

            // Ativar blur effect
            if (blurEffect != null) blurEffect.SetActive(true);

            float elapsed = 0f;
            float accelerationTime = 0.5f;

            // Fase de aceleração
            while (elapsed < accelerationTime)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / accelerationTime;
                currentSpeed = Mathf.Lerp(0f, spinSpeed, spinCurve.Evaluate(t));

                // Movimento vertical para simular spin
                float movement = currentSpeed * Time.deltaTime;
                transform.Translate(Vector3.down * movement, Space.World);

                yield return null;
            }

            currentSpeed = spinSpeed;
        }

        /// <summary>
        /// Para a animação de spin com easing suave
        /// </summary>
        public void StopSpin(float decelerationTime = 1f)
        {
            if (!isSpinning) return;
            StartCoroutine(StopAnimation(decelerationTime));
        }

        private IEnumerator StopAnimation(float decelerationTime)
        {
            float elapsed = 0f;
            float initialSpeed = currentSpeed;

            // Fase de desaceleração
            while (elapsed < decelerationTime)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / decelerationTime;
                currentSpeed = Mathf.Lerp(initialSpeed, 0f, stopCurve.Evaluate(t));

                float movement = currentSpeed * Time.deltaTime;
                transform.Translate(Vector3.down * movement, Space.World);

                yield return null;
            }

            currentSpeed = 0f;

            // Desativar blur
            if (blurEffect != null) blurEffect.SetActive(false);

            // Animação de bounce ao parar
            yield return StartCoroutine(BounceStopAnimation());

            // Resetar posição
            transform.localPosition = originalPosition;
            isSpinning = false;
        }

        /// <summary>
        /// Animação de antecipação antes do spin
        /// </summary>
        private IEnumerator AnticipationAnimation()
        {
            float duration = anticipationDelay;
            float elapsed = 0f;
            Vector3 anticipationOffset = Vector3.down * 0.1f;

            // Move ligeiramente para baixo
            while (elapsed < duration * 0.5f)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / (duration * 0.5f);
                transform.localPosition = Vector3.Lerp(originalPosition, originalPosition + anticipationOffset, t);
                yield return null;
            }

            // Move de volta
            elapsed = 0f;
            while (elapsed < duration * 0.5f)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / (duration * 0.5f);
                transform.localPosition = Vector3.Lerp(originalPosition + anticipationOffset, originalPosition, t);
                yield return null;
            }

            transform.localPosition = originalPosition;
        }

        /// <summary>
        /// Animação de bounce ao parar
        /// </summary>
        private IEnumerator BounceStopAnimation()
        {
            float duration = 0.4f;
            float elapsed = 0f;
            int bounces = 2;

            for (int i = 0; i < bounces; i++)
            {
                elapsed = 0f;
                float currentBounce = bounceAmount * Mathf.Pow(bounceDecay, i);

                // Bounce para baixo
                while (elapsed < duration / (2 * bounces))
                {
                    elapsed += Time.deltaTime;
                    float t = elapsed / (duration / (2 * bounces));
                    float offset = Mathf.Sin(t * Mathf.PI) * currentBounce;
                    transform.localPosition = originalPosition + Vector3.down * offset;
                    yield return null;
                }
            }

            transform.localPosition = originalPosition;
        }

        /// <summary>
        /// Para imediatamente e reseta
        /// </summary>
        public void ForceStop()
        {
            StopAllCoroutines();
            isSpinning = false;
            currentSpeed = 0f;
            transform.localPosition = originalPosition;
            
            if (blurEffect != null) blurEffect.SetActive(false);
        }

        /// <summary>
        /// Animação de "quase acertou" (near miss)
        /// </summary>
        public IEnumerator PlayNearMissAnimation()
        {
            // Parar um pouco antes e depois ajustar
            yield return StopAnimation(0.8f);
            
            // Pequeno ajuste para a posição final
            float nudgeDuration = 0.3f;
            float elapsed = 0f;
            Vector3 startPos = transform.localPosition;
            Vector3 targetPos = originalPosition;

            while (elapsed < nudgeDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / nudgeDuration;
                transform.localPosition = Vector3.Lerp(startPos, targetPos, stopCurve.Evaluate(t));
                yield return null;
            }

            transform.localPosition = originalPosition;
        }

        /// <summary>
        /// Aplica efeito de blur baseado na velocidade
        /// </summary>
        private void UpdateBlurEffect()
        {
            if (blurEffect == null) return;

            float normalizedSpeed = currentSpeed / spinSpeed;
            float blur = Mathf.Lerp(0f, blurAmount, normalizedSpeed);
            
            // Aplicar blur ao material (se tiver shader apropriado)
            SpriteRenderer sr = blurEffect.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Color color = sr.color;
                color.a = blur;
                sr.color = color;
            }
        }
    }
}
