// DevPanel.cs
// Painel dev simples usando OnGUI
using UnityEngine;
using Slots.Config;

namespace Slots.Dev
{
    public class DevPanel : MonoBehaviour
    {
        public GameObject root;
        public GameSettings settings;
        public KeyCode toggleKey = KeyCode.F1;
        private bool open;

        private void Update()
        {
            if (Input.GetKeyDown(toggleKey)) open = !open;
            if (root) root.SetActive(open);
        }

        private void OnGUI()
        {
            if (!open || settings == null) return;
            GUILayout.BeginArea(new Rect(10, 10, 260, 200), GUI.skin.window);
            GUILayout.Label("Dev Panel");
            GUILayout.Label($"RTP: {settings.targetRTP:0.0}%");
            settings.targetRTP = GUILayout.HorizontalSlider(settings.targetRTP, 85f, 97f);
            GUILayout.Label($"Lines: {settings.numberOfPaylines}");
            settings.numberOfPaylines = Mathf.RoundToInt(GUILayout.HorizontalSlider(settings.numberOfPaylines, 20, 25));
            GUILayout.Label($"Volatility: {settings.volatilityMode}");
            if (GUILayout.Button("Vol Low")) settings.volatilityMode = VolatilityMode.Low;
            if (GUILayout.Button("Vol Med")) settings.volatilityMode = VolatilityMode.Medium;
            if (GUILayout.Button("Vol High")) settings.volatilityMode = VolatilityMode.High;
            GUILayout.EndArea();
        }
    }
}
