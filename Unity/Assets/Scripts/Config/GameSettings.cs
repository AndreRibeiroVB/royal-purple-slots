// GameSettings.cs
// Responsável por centralizar configurações do jogo para edição via Inspector
using UnityEngine;

namespace Slots.Config
{
    public enum VolatilityMode { Low, Medium, High }

    public class GameSettings : MonoBehaviour
    {
        [Header("Economia")] 
        [Range(85f, 97f)] public float targetRTP = 95f;
        public VolatilityMode volatilityMode = VolatilityMode.Medium;
        [Range(20, 25)] public int numberOfPaylines = 25;
        [Min(1)] public int linesDefault = 25;
        [Min(1)] public int bonusTriggerCount = 3;
        [Min(1)] public int initialBonusSpins = 3;
        [Min(1)] public int autoplaySpins = 0;
        public float defaultBet = 1f;
        public float startingBalance = 1000f;

        [Header("Bonus Pot - Valores fixos (multiplicadores do bet)")]
        public int miniPot = 20;
        public int majorPot = 100;
        public int grandPot = 500;
        public int ultraPot = 1000;

        [Header("Demo JSON")] 
        [Tooltip("Se vazio, usa Resources/DemoSamples/sample_demo.json")] 
        public string demoJSONUrl = "";

        [Header("RNG")] public int seed = 12345; // determinístico para QA

        [Header("Splash Screen Settings")]
        public bool enableSplashScreen = true;
        [Range(1f, 5f)] public float providerLogoDuration = 2f;
        [Range(1f, 5f)] public float loadingDuration = 2.5f;
        public bool allowSkipSplash = true;

        public void ApplyRuntimeAdjust(float rtp, VolatilityMode vol, int lines)
        {
            targetRTP = Mathf.Clamp(rtp, 85f, 97f);
            volatilityMode = vol;
            numberOfPaylines = Mathf.Clamp(lines, 20, 25);
        }
    }
}
