// PoolManager.cs
// Pooling simples para performance
using System.Collections.Generic;
using UnityEngine;

namespace Slots.Core
{
    public class PoolManager : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string key;
            public GameObject prefab;
            public int initial = 5;
        }

        public List<Pool> pools = new List<Pool>();
        private Dictionary<string, Queue<GameObject>> map = new Dictionary<string, Queue<GameObject>>();

        private void Awake()
        {
            foreach (var p in pools)
            {
                var q = new Queue<GameObject>();
                for (int i = 0; i < p.initial; i++)
                {
                    var go = Instantiate(p.prefab, transform);
                    go.SetActive(false);
                    q.Enqueue(go);
                }
                map[p.key] = q;
            }
        }

        public GameObject Spawn(string key, Vector3 pos, Quaternion rot)
        {
            if (!map.ContainsKey(key) || map[key].Count == 0) return null;
            var go = map[key].Dequeue();
            go.transform.SetPositionAndRotation(pos, rot);
            go.SetActive(true);
            return go;
        }

        public void Despawn(string key, GameObject go)
        {
            go.SetActive(false);
            if (!map.ContainsKey(key)) map[key] = new Queue<GameObject>();
            map[key].Enqueue(go);
        }
    }
}
