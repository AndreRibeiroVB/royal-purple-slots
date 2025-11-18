// BonusPotController.cs
// Mecânica Hold & Win simplificada
using System.Collections.Generic;
using UnityEngine;
using Slots.Services;
using Slots.Config;

namespace Slots.Bonus
{
    public enum CoinTier { Bronze, Silver, Gold, Mega }

    public class BonusPotController : MonoBehaviour
    {
        [Header("Estado")] public bool inBonus;
        public int spinsLeft;
        public int cols = 5, rows = 3;
        public bool[,] filled;
        public int totalCollected;

        [Header("Refs")] public RNGService rng;
        public GameSettings settings;

        public System.Action OnBonusStart;
        public System.Action OnBonusEnd;

        private void Awake()
        {
            filled = new bool[cols, rows];
        }

        public void StartBonus()
        {
            inBonus = true;
            spinsLeft = Mathf.Max(1, settings.initialBonusSpins);
            filled = new bool[cols, rows];
            totalCollected = 0;
            OnBonusStart?.Invoke();
        }

        public void StepBonus(float bet)
        {
            if (!inBonus) return;
            bool anyNew = false;
            // cada rodada tenta colocar 0-3 moedas aleatórias
            int attempts = rng.NextInt(0, 4);
            for (int i = 0; i < attempts; i++)
            {
                int x = rng.NextInt(0, cols);
                int y = rng.NextInt(0, rows);
                if (!filled[x, y])
                {
                    filled[x, y] = true;
                    int mult = RollCoinMultiplier();
                    totalCollected += Mathf.RoundToInt(bet * mult);
                    anyNew = true;
                }
            }

            spinsLeft = anyNew ? settings.initialBonusSpins : (spinsLeft - 1);
            if (AllFilled() || spinsLeft <= 0)
            {
                EndBonus();
            }
        }

        private int RollCoinMultiplier()
        {
            // Probabilidades simples por volatilidade
            int roll = rng.NextInt(0, 100);
            if (roll < 60) return 1; // bronze
            if (roll < 85) return 5; // silver
            if (roll < 97) return 10; // gold
            return 25; // mega
        }

        private bool AllFilled()
        {
            for (int x = 0; x < cols; x++)
                for (int y = 0; y < rows; y++)
                    if (!filled[x, y]) return false;
            return true;
        }

        private void EndBonus()
        {
            inBonus = false;
            OnBonusEnd?.Invoke();
        }
    }
}
