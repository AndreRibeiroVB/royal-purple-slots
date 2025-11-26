// TitleScreenController.cs
// Controla elementos da tela de título: logo animado, botão START, partículas de fundo
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Slots.UI
{
    public class TitleScreenController : MonoBehaviour
    {
        [Header("Logo Animation")]
        [SerializeField] private Image gameLogo;
        [SerializeField] private float logoFloatSpeed = 1.5f;
        [SerializeField] private float logoFloatAmount = 10f;

        [Header("Start Button")]
        [SerializeField] private Button startButton;
        [SerializeField] private float buttonPulseSpeed = 2f;
        [SerializeField] private float buttonPulseScale = 1.1f;

        [Header("Particles")]
        [SerializeField] private ParticleSystem backgroundParticles;

        [Header("Glow Effects")]
        [SerializeField] private Image logoGlow;
        [SerializeField] private Image buttonGlow;

        private Vector3 originalLogoPosition;
        private Vector3 originalButtonScale;

        private void Start()
        {
            if (gameLogo)
            {
                originalLogoPosition = gameLogo.transform.localPosition;
                StartCoroutine(AnimateLogo());
            }

            if (startButton)
            {
                originalButtonScale = startButton.transform.localScale;
                StartCoroutine(AnimateButton());
            }

            if (backgroundParticles)
            {
                backgroundParticles.Play();
            }

            if (logoGlow)
            {
                StartCoroutine(AnimateGlow(logoGlow));
            }

            if (buttonGlow)
            {
                StartCoroutine(AnimateGlow(buttonGlow));
            }
        }

        private IEnumerator AnimateLogo()
        {
            while (true)
            {
                float y = originalLogoPosition.y + Mathf.Sin(Time.time * logoFloatSpeed) * logoFloatAmount;
                gameLogo.transform.localPosition = new Vector3(
                    originalLogoPosition.x, 
                    y, 
                    originalLogoPosition.z
                );
                yield return null;
            }
        }

        private IEnumerator AnimateButton()
        {
            while (true)
            {
                float scale = 1f + (Mathf.Sin(Time.time * buttonPulseSpeed) * 0.5f + 0.5f) * (buttonPulseScale - 1f);
                startButton.transform.localScale = originalButtonScale * scale;
                yield return null;
            }
        }

        private IEnumerator AnimateGlow(Image glowImage)
        {
            Color color = glowImage.color;
            float baseAlpha = color.a;

            while (true)
            {
                float alpha = baseAlpha * (0.5f + 0.5f * Mathf.Sin(Time.time * 2f));
                color.a = alpha;
                glowImage.color = color;
                yield return null;
            }
        }

        public void PlayClickSound()
        {
            // Hook para SoundManager
            var soundManager = FindAnyObjectByType<SoundManager>();
            if (soundManager)
            {
                soundManager.PlaySound("ButtonClick");
            }
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
