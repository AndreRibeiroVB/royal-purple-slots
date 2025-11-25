// BigWinPopup.cs
// Popup 3D animado para Big Wins com efeitos especiais
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Slots.Anim
{
    public class BigWinPopup : MonoBehaviour
    {
        [Header("UI References")]
        public Canvas popupCanvas;
        public Text winAmountText;
        public Text titleText;
        public GameObject rayBurstEffect;
        public GameObject particleContainer;

        [Header("Animation Settings")]
        public float scaleInDuration = 0.5f;
        public float displayDuration = 3f;
        public float scaleOutDuration = 0.3f;
        public AnimationCurve scaleInCurve = AnimationCurve.EaseOutBack(0, 0, 1, 1);
        public AnimationCurve scaleOutCurve = AnimationCurve.EaseInBack(0, 1, 1, 0);

        [Header("3D Effect Settings")]
        public float rotationSpeed = 30f;
        public float pulseScale = 1.1f;
        public float pulseSpeed = 2f;

        [Header("Thresholds")]
        public float bigWinMultiplier = 3f;
        public float megaWinMultiplier = 10f;
        public float epicWinMultiplier = 25f;

        [Header("Colors")]
        public Color bigWinColor = new Color(1f, 0.84f, 0f); // Gold
        public Color megaWinColor = new Color(1f, 0.27f, 0f); // Orange
        public Color epicWinColor = new Color(0.58f, 0f, 0.83f); // Purple

        private ParticleWinSystem particleSystem;
        private Vector3 originalScale;
        private bool isShowing = false;

        private void Awake()
        {
            if (popupCanvas != null)
            {
                popupCanvas.gameObject.SetActive(false);
                originalScale = popupCanvas.transform.localScale;
            }

            particleSystem = FindObjectOfType<ParticleWinSystem>();
        }

        /// <summary>
        /// Mostra o popup de Big Win
        /// </summary>
        public void ShowBigWin(float winAmount, float betAmount)
        {
            if (isShowing) return;
            StartCoroutine(BigWinAnimation(winAmount, betAmount));
        }

        private IEnumerator BigWinAnimation(float winAmount, float betAmount)
        {
            isShowing = true;
            float multiplier = winAmount / betAmount;

            // Determinar o tipo de win
            string winType;
            Color winColor;

            if (multiplier >= epicWinMultiplier)
            {
                winType = "EPIC WIN!!!";
                winColor = epicWinColor;
            }
            else if (multiplier >= megaWinMultiplier)
            {
                winType = "MEGA WIN!!";
                winColor = megaWinColor;
            }
            else
            {
                winType = "BIG WIN!";
                winColor = bigWinColor;
            }

            // Configurar UI
            if (titleText != null)
            {
                titleText.text = winType;
                titleText.color = winColor;
            }

            if (winAmountText != null)
            {
                winAmountText.text = "";
            }

            // Ativar canvas
            if (popupCanvas != null)
            {
                popupCanvas.gameObject.SetActive(true);
                popupCanvas.transform.localScale = Vector3.zero;
            }

            // Emitir partículas de fundo
            if (particleSystem != null)
            {
                particleSystem.EmitWinParticles(winAmount, Vector3.zero);
            }

            // Ativar ray burst
            if (rayBurstEffect != null)
            {
                rayBurstEffect.SetActive(true);
            }

            // Animação de entrada com escala
            yield return StartCoroutine(ScaleInAnimation());

            // Rotação do ray burst
            if (rayBurstEffect != null)
            {
                StartCoroutine(RotateRayBurst());
            }

            // Counter animation do valor
            yield return StartCoroutine(CountUpAnimation(winAmount));

            // Pulse animation durante display
            StartCoroutine(PulseAnimation());

            // Esperar antes de fechar
            yield return new WaitForSeconds(displayDuration);

            // Animação de saída
            yield return StartCoroutine(ScaleOutAnimation());

            // Desativar
            if (popupCanvas != null)
            {
                popupCanvas.gameObject.SetActive(false);
            }

            if (rayBurstEffect != null)
            {
                rayBurstEffect.SetActive(false);
            }

            isShowing = false;
        }

        private IEnumerator ScaleInAnimation()
        {
            float elapsed = 0f;

            while (elapsed < scaleInDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / scaleInDuration;
                float scale = scaleInCurve.Evaluate(t);

                if (popupCanvas != null)
                {
                    popupCanvas.transform.localScale = originalScale * scale;
                }

                yield return null;
            }

            if (popupCanvas != null)
            {
                popupCanvas.transform.localScale = originalScale;
            }
        }

        private IEnumerator ScaleOutAnimation()
        {
            float elapsed = 0f;

            while (elapsed < scaleOutDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / scaleOutDuration;
                float scale = scaleOutCurve.Evaluate(t);

                if (popupCanvas != null)
                {
                    popupCanvas.transform.localScale = originalScale * scale;
                }

                yield return null;
            }
        }

        private IEnumerator CountUpAnimation(float targetAmount)
        {
            if (winAmountText == null) yield break;

            float duration = 1.5f;
            float elapsed = 0f;
            float currentAmount = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                
                currentAmount = Mathf.Lerp(0f, targetAmount, t);
                winAmountText.text = $"${currentAmount:F2}";

                yield return null;
            }

            winAmountText.text = $"${targetAmount:F2}";
        }

        private IEnumerator PulseAnimation()
        {
            if (popupCanvas == null) yield break;

            float duration = displayDuration;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                
                float pulse = 1f + (Mathf.Sin(elapsed * pulseSpeed * Mathf.PI * 2f) * (pulseScale - 1f));
                popupCanvas.transform.localScale = originalScale * pulse;

                yield return null;
            }

            popupCanvas.transform.localScale = originalScale;
        }

        private IEnumerator RotateRayBurst()
        {
            if (rayBurstEffect == null) yield break;

            while (rayBurstEffect.activeSelf)
            {
                rayBurstEffect.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
                yield return null;
            }
        }

        /// <summary>
        /// Força o fechamento do popup
        /// </summary>
        public void ForceClose()
        {
            StopAllCoroutines();
            
            if (popupCanvas != null)
            {
                popupCanvas.gameObject.SetActive(false);
            }

            if (rayBurstEffect != null)
            {
                rayBurstEffect.SetActive(false);
            }

            isShowing = false;
        }
    }
}
