// ReelController.cs
// Controla lógica de rolos (simulação). Em produção, animar via Animator/Timeline.
using System.Collections.Generic;
using UnityEngine;
using Slots.Services;

namespace Slots.Slots
{
    public class ReelController : MonoBehaviour
    {
        public int reelIndex;
        public float spinTime = 1.2f;
        public float stopDelay = 0.2f;
        public List<SymbolType> strip = new List<SymbolType>();
        public SymbolType currentTop, currentMid, currentBot;

        private RNGService rng;

        public void Init(RNGService rngService, List<SymbolType> symbolSet)
        {
            rng = rngService;
            if (strip == null || strip.Count == 0)
            {
                strip = symbolSet;
            }
            RandomizeVisible();
        }

        public void RandomizeVisible()
        {
            currentTop = strip[rng.NextInt(0, strip.Count)];
            currentMid = strip[rng.NextInt(0, strip.Count)];
            currentBot = strip[rng.NextInt(0, strip.Count)];
        }

        public void ApplyResult(SymbolType top, SymbolType mid, SymbolType bot)
        {
            currentTop = top; currentMid = mid; currentBot = bot;
        }

        public SymbolType[] GetVisible() => new[] { currentTop, currentMid, currentBot };
    }
}
