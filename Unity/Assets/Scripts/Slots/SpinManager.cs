// SpinManager.cs
// Núcleo da rotação: gera grid, avalia ganhos, integra bônus e free spins
using System.Collections.Generic;
using UnityEngine;
using Slots.Services;
using Slots.Slots;
using Slots.Bonus;
using Slots.UI;
using Slots.Config;
using Slots.Anim;

namespace Slots.Slots
{
    public class SpinManager : MonoBehaviour
    {
        [Header("Refs")]
        public RNGService rng;
        public SymbolManager symbols;
        public PaylinesManager paylines;
        public UIManager ui;
        public BonusPotController bonus;
        public FreeSpinsController freeSpins;
        public GameSettings settings;
        public AnimationManager anim;

        [Header("Reels")]
        public ReelController[] reels = new ReelController[5];

        [Header("Estado")] public bool isSpinning;

        private Dictionary<SymbolType, int> currentWeights;

        private void Start()
        {
            currentWeights = symbols.GetWeights()[settings.volatilityMode];
            var all = new List<SymbolType>((SymbolType[])System.Enum.GetValues(typeof(SymbolType)));
            foreach (var r in reels) r.Init(rng, all);
        }

        public void Spin()
        {
            if (isSpinning || (bonus && bonus.inBonus)) return;
            if (ui) ui.DeductBet();
            isSpinning = true;

            var grid = GenerateGrid();
            ApplyToReels(grid);
            ResolveSpin(grid);
            isSpinning = false;
        }

        private SymbolType[][] GenerateGrid()
        {
            var grid = new SymbolType[5][];
            for (int reel = 0; reel < 5; reel++)
            {
                grid[reel] = new SymbolType[3];
                for (int row = 0; row < 3; row++)
                {
                    grid[reel][row] = RollSymbol();
                }
            }
            return grid;
        }

        private SymbolType RollSymbol()
        {
            int total = 0; foreach (var kv in currentWeights) total += kv.Value;
            int r = rng.NextInt(0, total);
            int cum = 0;
            foreach (var kv in currentWeights)
            {
                cum += kv.Value;
                if (r < cum) return kv.Key;
            }
            return SymbolType.J;
        }

        private void ApplyToReels(SymbolType[][] grid)
        {
            for (int reel = 0; reel < 5; reel++)
            {
                reels[reel].ApplyResult(grid[reel][0], grid[reel][1], grid[reel][2]);
            }
        }

        private void ResolveSpin(SymbolType[][] grid)
        {
            // Scatters
            int scatters = CountSymbol(grid, SymbolType.Scatter);
            if (scatters >= 3)
            {
                freeSpins?.StartFreeSpins(10);
                ui?.ShowFreeSpinsUI(true);
            }

            // Bonus trigger
            int bonusCoins = CountSymbol(grid, SymbolType.BonusCoin);
            if (bonusCoins >= settings.bonusTriggerCount)
            {
                ui?.ShowBonusTransition();
                bonus?.StartBonus();
                // Simular algumas rodadas de bônus imediatamente para demo
                while (bonus.inBonus)
                {
                    bonus.StepBonus(ui ? ui.bet : 1f);
                }
                // coleta
                if (ui) ui.AddWin(bonus.totalCollected);
            }

            // Paylines
            var lineWin = paylines.EvaluateGrid(grid, symbols.GetLinePayout, symbols.IsWild, out var wins);
            bool hadWin = lineWin > 0;
            int mult = freeSpins && freeSpins.inFreeSpins ? freeSpins.multiplier : 1;
            int finalWin = lineWin * mult;

            if (finalWin > 0)
            {
                ui?.AddWin(finalWin * (ui ? ui.bet : 1f));
                anim?.HighlightWinPositions(wins.Count > 0 ? wins[0].positions : new List<Vector2Int>());
                if (finalWin >= 50) anim?.ShowBigWin(finalWin);
            }
            freeSpins?.RegisterSpinResult(hadWin);
            if (freeSpins && !freeSpins.inFreeSpins) ui?.ShowFreeSpinsUI(false);
        }

        private int CountSymbol(SymbolType[][] grid, SymbolType s)
        {
            int c = 0;
            for (int r = 0; r < 5; r++)
                for (int y = 0; y < 3; y++)
                    if (grid[r][y] == s) c++;
            return c;
        }
    }
}
