// SplashScreenManager.cs
// Gerencia a sequência de telas de abertura: Logo Provider → Loading → Title Screen → Main Game
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Slots.UI
{
    public class SplashScreenManager : MonoBehaviour
    {
        [Header("Screens")]
        [SerializeField] private GameObject providerLogoScreen;
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private GameObject titleScreen;

        [Header("Provider Logo")]
        [SerializeField] private Image providerLogo;
        [SerializeField] private float providerLogoDuration = 2f;

        [Header("Loading")]
        [SerializeField] private LoadingProgressBar progressBar;
        [SerializeField] private float loadingDuration = 2.5f;

        [Header("Title Screen")]
        [SerializeField] private Button startButton;
        [SerializeField] private Image gameLogo;
        [SerializeField] private CanvasGroup titleCanvasGroup;

        [Header("Settings")]
        [SerializeField] private bool allowSkip = true;
        [SerializeField] private float fadeSpeed = 1f;

        private bool isSkipped = false;

        private void Start()
        {
            // Hide all screens initially
            providerLogoScreen.SetActive(false);
            loadingScreen.SetActive(false);
            titleScreen.SetActive(false);

            // Setup start button
            if (startButton)
            {
                startButton.onClick.AddListener(OnStartButtonClicked);
            }

            // Start the splash sequence
            StartCoroutine(SplashSequence());
        }

        private void Update()
        {
            // Allow skip with any key/click
            if (allowSkip && !isSkipped && Input.anyKeyDown)
            {
                isSkipped = true;
                StopAllCoroutines();
                StartCoroutine(SkipToTitleScreen());
            }
        }

        private IEnumerator SplashSequence()
        {
            // 1. Show Provider Logo
            yield return StartCoroutine(ShowProviderLogo());

            // 2. Show Loading Screen
            yield return StartCoroutine(ShowLoadingScreen());

            // 3. Show Title Screen
            yield return StartCoroutine(ShowTitleScreen());
        }

        private IEnumerator ShowProviderLogo()
        {
            providerLogoScreen.SetActive(true);

            // Fade in logo
            if (providerLogo)
            {
                Color color = providerLogo.color;
                color.a = 0f;
                providerLogo.color = color;

                float elapsed = 0f;
                while (elapsed < fadeSpeed)
                {
                    elapsed += Time.deltaTime;
                    color.a = Mathf.Lerp(0f, 1f, elapsed / fadeSpeed);
                    providerLogo.color = color;
                    yield return null;
                }
            }

            // Wait
            yield return new WaitForSeconds(providerLogoDuration - fadeSpeed * 2);

            // Fade out logo
            if (providerLogo)
            {
                Color color = providerLogo.color;
                float elapsed = 0f;
                while (elapsed < fadeSpeed)
                {
                    elapsed += Time.deltaTime;
                    color.a = Mathf.Lerp(1f, 0f, elapsed / fadeSpeed);
                    providerLogo.color = color;
                    yield return null;
                }
            }

            providerLogoScreen.SetActive(false);
        }

        private IEnumerator ShowLoadingScreen()
        {
            loadingScreen.SetActive(true);

            // Simulate loading with progress bar
            if (progressBar)
            {
                yield return StartCoroutine(progressBar.AnimateProgress(loadingDuration));
            }
            else
            {
                yield return new WaitForSeconds(loadingDuration);
            }

            loadingScreen.SetActive(false);
        }

        private IEnumerator ShowTitleScreen()
        {
            titleScreen.SetActive(true);

            // Fade in title screen
            if (titleCanvasGroup)
            {
                titleCanvasGroup.alpha = 0f;
                float elapsed = 0f;
                while (elapsed < fadeSpeed)
                {
                    elapsed += Time.deltaTime;
                    titleCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeSpeed);
                    yield return null;
                }
            }

            // Animate game logo scale
            if (gameLogo)
            {
                StartCoroutine(AnimateLogoScale());
            }

            // Enable start button
            if (startButton)
            {
                startButton.interactable = true;
            }
        }

        private IEnumerator AnimateLogoScale()
        {
            Vector3 originalScale = gameLogo.transform.localScale;
            float pulseSpeed = 1.5f;

            while (true)
            {
                float scale = 1f + Mathf.Sin(Time.time * pulseSpeed) * 0.05f;
                gameLogo.transform.localScale = originalScale * scale;
                yield return null;
            }
        }

        private IEnumerator SkipToTitleScreen()
        {
            // Hide provider and loading screens
            providerLogoScreen.SetActive(false);
            loadingScreen.SetActive(false);

            // Show title screen immediately
            yield return StartCoroutine(ShowTitleScreen());
        }

        private void OnStartButtonClicked()
        {
            StartCoroutine(TransitionToMainGame());
        }

        private IEnumerator TransitionToMainGame()
        {
            // Disable button to prevent double-click
            if (startButton)
            {
                startButton.interactable = false;
            }

            // Fade out title screen
            if (titleCanvasGroup)
            {
                float elapsed = 0f;
                while (elapsed < fadeSpeed)
                {
                    elapsed += Time.deltaTime;
                    titleCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeSpeed);
                    yield return null;
                }
            }

            // Load main scene
            SceneManager.LoadScene("Main");
        }
    }
}
