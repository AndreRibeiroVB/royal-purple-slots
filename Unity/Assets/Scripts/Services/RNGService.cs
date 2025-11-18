// RNGService.cs
// Serviço determinístico de RNG com seed para QA e replays
using UnityEngine;

namespace Slots.Services
{
    public class RNGService : MonoBehaviour
    {
        [SerializeField] private int seed = 12345;
        private System.Random rng;

        private void Awake()
        {
            rng = new System.Random(seed);
        }

        public void SetSeed(int newSeed)
        {
            seed = newSeed;
            rng = new System.Random(seed);
        }

        public int NextInt(int minInclusive, int maxExclusive)
        {
            return rng.Next(minInclusive, maxExclusive);
        }

        public float NextFloat()
        {
            return (float)rng.NextDouble();
        }

        public bool Chance(float probability01)
        {
            return NextFloat() < Mathf.Clamp01(probability01);
        }
    }
}
