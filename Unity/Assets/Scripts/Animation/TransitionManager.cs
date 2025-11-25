// TransitionManager.cs
// Gerencia transições suaves entre modos de jogo (Main, Bonus, FreeSpins)
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Slots.Anim
{
    public class TransitionManager : MonoBehaviour
    {
        [Header("Transition Settings")]
        public float transitionDuration = 1f;
        public AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        public AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 1, 1, 1.2f);

        [Header("Overlay")]
        public GameObject transitionOverlay;
        public Image fadeImage;
        public Color fadeColor = Color.black;

        [Header("Effects")]
        public GameObject portalEffect;
        public GameObject lightRaysEffect;
        public ParticleSystem transitionParticles;

        [Header("Audio")]
        public AudioClip transitionSound;
        private AudioSource audioSource;

        private bool isTransitioning = false;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            
            if (transitionOverlay != null)
            {
                transitionOverlay.SetActive(false);
            }

            // Garantir que o TransitionManager persista entre cenas
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Transição para o modo Bonus
        /// </summary>
        public void TransitionToBonus()
        {
            if (isTransitioning) return;
            StartCoroutine(TransitionAnimation("Bonus", TransitionStyle.Portal));
        }

        /// <summary>
        /// Transição para Free Spins
        /// </summary>
        public void TransitionToFreeSpins()
        {
            if (isTransitioning) return;
            StartCoroutine(TransitionAnimation("FreeSpins", TransitionStyle.LightRays));
        }

        /// <summary>
        /// Retorna ao modo Main
        /// </summary>
        public void TransitionToMain()
        {
            if (isTransitioning) return;
            StartCoroutine(TransitionAnimation("Main", TransitionStyle.Fade));
        }

        public enum TransitionStyle
        {
            Fade,
            Portal,
            LightRays,
            Zoom
        }

        private IEnumerator TransitionAnimation(string targetScene, TransitionStyle style)
        {
            isTransitioning = true;

            // Ativar overlay
            if (transitionOverlay != null)
            {
                transitionOverlay.SetActive(true);
            }

            // Tocar som
            if (audioSource != null && transitionSound != null)
            {
                audioSource.PlayOneShot(transitionSound);
            }

            // Animação de saída baseada no estilo
            yield return StartCoroutine(PlayExitAnimation(style));

            // Carregar nova cena
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);
            asyncLoad.allowSceneActivation = false;

            // Esperar carregar
            while (asyncLoad.progress < 0.9f)
            {
                yield return null;
            }

            // Ativar nova cena
            asyncLoad.allowSceneActivation = true;

            // Esperar cena estar pronta
            yield return new WaitForSeconds(0.1f);

            // Animação de entrada
            yield return StartCoroutine(PlayEnterAnimation(style));

            // Desativar overlay
            if (transitionOverlay != null)
            {
                transitionOverlay.SetActive(false);
            }

            isTransitioning = false;
        }

        private IEnumerator PlayExitAnimation(TransitionStyle style)
        {
            switch (style)
            {
                case TransitionStyle.Fade:
                    yield return StartCoroutine(FadeOut());
                    break;
                case TransitionStyle.Portal:
                    yield return StartCoroutine(PortalOut());
                    break;
                case TransitionStyle.LightRays:
                    yield return StartCoroutine(LightRaysOut());
                    break;
                case TransitionStyle.Zoom:
                    yield return StartCoroutine(ZoomOut());
                    break;
            }
        }

        private IEnumerator PlayEnterAnimation(TransitionStyle style)
        {
            switch (style)
            {
                case TransitionStyle.Fade:
                    yield return StartCoroutine(FadeIn());
                    break;
                case TransitionStyle.Portal:
                    yield return StartCoroutine(PortalIn());
                    break;
                case TransitionStyle.LightRays:
                    yield return StartCoroutine(LightRaysIn());
                    break;
                case TransitionStyle.Zoom:
                    yield return StartCoroutine(ZoomIn());
                    break;
            }
        }

        #region Fade Transitions
        private IEnumerator FadeOut()
        {
            if (fadeImage == null) yield break;

            float elapsed = 0f;
            Color color = fadeColor;

            while (elapsed < transitionDuration / 2f)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / (transitionDuration / 2f);
                
                color.a = fadeCurve.Evaluate(t);
                fadeImage.color = color;

                yield return null;
            }

            color.a = 1f;
            fadeImage.color = color;
        }

        private IEnumerator FadeIn()
        {
            if (fadeImage == null) yield break;

            float elapsed = 0f;
            Color color = fadeColor;
            color.a = 1f;

            while (elapsed < transitionDuration / 2f)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / (transitionDuration / 2f);
                
                color.a = 1f - fadeCurve.Evaluate(t);
                fadeImage.color = color;

                yield return null;
            }

            color.a = 0f;
            fadeImage.color = color;
        }
        #endregion

        #region Portal Transitions
        private IEnumerator PortalOut()
        {
            if (portalEffect != null)
            {
                portalEffect.SetActive(true);
            }

            if (transitionParticles != null)
            {
                transitionParticles.Play();
            }

            // Zoom para o centro com escala
            yield return StartCoroutine(ZoomToCenter(transitionDuration / 2f));

            yield return StartCoroutine(FadeOut());
        }

        private IEnumerator PortalIn()
        {
            yield return StartCoroutine(FadeIn());

            // Zoom do centro para fora
            yield return StartCoroutine(ZoomFromCenter(transitionDuration / 2f));

            if (portalEffect != null)
            {
                portalEffect.SetActive(false);
            }
        }
        #endregion

        #region Light Rays Transitions
        private IEnumerator LightRaysOut()
        {
            if (lightRaysEffect != null)
            {
                lightRaysEffect.SetActive(true);
            }

            yield return StartCoroutine(FadeOut());

            // Rotacionar rays
            float elapsed = 0f;
            while (elapsed < transitionDuration / 2f)
            {
                if (lightRaysEffect != null)
                {
                    lightRaysEffect.transform.Rotate(Vector3.forward, 360f * Time.deltaTime);
                }
                elapsed += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator LightRaysIn()
        {
            yield return StartCoroutine(FadeIn());

            if (lightRaysEffect != null)
            {
                lightRaysEffect.SetActive(false);
            }
        }
        #endregion

        #region Zoom Transitions
        private IEnumerator ZoomOut()
        {
            yield return StartCoroutine(ZoomToCenter(transitionDuration));
        }

        private IEnumerator ZoomIn()
        {
            yield return StartCoroutine(ZoomFromCenter(transitionDuration));
        }

        private IEnumerator ZoomToCenter(float duration)
        {
            Camera mainCam = Camera.main;
            if (mainCam == null) yield break;

            float elapsed = 0f;
            float originalSize = mainCam.orthographicSize;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                
                float scale = scaleCurve.Evaluate(t);
                mainCam.orthographicSize = originalSize * scale;

                yield return null;
            }
        }

        private IEnumerator ZoomFromCenter(float duration)
        {
            Camera mainCam = Camera.main;
            if (mainCam == null) yield break;

            float elapsed = 0f;
            float targetSize = mainCam.orthographicSize;
            float startSize = targetSize * 1.2f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                
                mainCam.orthographicSize = Mathf.Lerp(startSize, targetSize, fadeCurve.Evaluate(t));

                yield return null;
            }

            mainCam.orthographicSize = targetSize;
        }
        #endregion

        /// <summary>
        /// Para transição imediatamente
        /// </summary>
        public void CancelTransition()
        {
            StopAllCoroutines();
            
            if (transitionOverlay != null)
            {
                transitionOverlay.SetActive(false);
            }

            if (portalEffect != null)
            {
                portalEffect.SetActive(false);
            }

            if (lightRaysEffect != null)
            {
                lightRaysEffect.SetActive(false);
            }

            isTransitioning = false;
        }
    }
}
