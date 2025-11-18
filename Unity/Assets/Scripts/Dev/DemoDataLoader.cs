// DemoDataLoader.cs
// Carrega JSON de demonstração via Resources ou URL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Slots.Dev
{
    [System.Serializable]
    public class DemoSpin
    {
        public string spinId;
        public float bet;
        public int lines;
        public List<string> result;
        public float win;
    }

    [System.Serializable]
    public class DemoWin
    {
        public string user;
        public float win;
        public string time;
    }

    [System.Serializable]
    public class DemoData
    {
        public List<DemoSpin> history = new List<DemoSpin>();
        public float rtp;
        public List<DemoWin> lastWins = new List<DemoWin>();
    }

    public class DemoDataLoader : MonoBehaviour
    {
        public string urlOrEmpty;
        public DemoData data;

        public IEnumerator Load()
        {
            if (!string.IsNullOrEmpty(urlOrEmpty))
            {
                using (var req = UnityWebRequest.Get(urlOrEmpty))
                {
                    yield return req.SendWebRequest();
                    if (req.result == UnityWebRequest.Result.Success)
                    {
                        data = JsonUtility.FromJson<DemoData>(req.downloadHandler.text);
                        Debug.Log("Demo loaded from URL");
                        yield break;
                    }
                }
            }
            var txt = Resources.Load<TextAsset>("DemoSamples/sample_demo");
            if (txt) data = JsonUtility.FromJson<DemoData>(txt.text);
            Debug.Log("Demo loaded from Resources");
        }
    }
}
