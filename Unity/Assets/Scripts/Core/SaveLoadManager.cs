// SaveLoadManager.cs
// Salva configs simples em PlayerPrefs
using UnityEngine;
using Slots.Config;

namespace Slots.Core
{
    public class SaveLoadManager : MonoBehaviour
    {
        private const string KEY_RTP = "CFG_RTP";
        private const string KEY_VOL = "CFG_VOL";
        private const string KEY_LINES = "CFG_LINES";

        public void Save(GameSettings gs)
        {
            PlayerPrefs.SetFloat(KEY_RTP, gs.targetRTP);
            PlayerPrefs.SetInt(KEY_VOL, (int)gs.volatilityMode);
            PlayerPrefs.SetInt(KEY_LINES, gs.numberOfPaylines);
            PlayerPrefs.Save();
        }

        public void Load(GameSettings gs)
        {
            if (PlayerPrefs.HasKey(KEY_RTP)) gs.targetRTP = PlayerPrefs.GetFloat(KEY_RTP);
            if (PlayerPrefs.HasKey(KEY_VOL)) gs.volatilityMode = (VolatilityMode)PlayerPrefs.GetInt(KEY_VOL);
            if (PlayerPrefs.HasKey(KEY_LINES)) gs.numberOfPaylines = PlayerPrefs.GetInt(KEY_LINES);
        }
    }
}
