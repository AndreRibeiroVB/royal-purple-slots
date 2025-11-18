// PaylinesManager.cs
// Define 25 linhas de pagamento e lógica de avaliação de ganhos
using System.Collections.Generic;
using UnityEngine;

namespace Slots.Slots
{
    public struct WinInfo
    {
        public SymbolType symbol;
        public int count;
        public int payout;
        public List<Vector2Int> positions; // (reel, row)
    }

    public class PaylinesManager : MonoBehaviour
    {
        public List<int[]> paylines = new List<int[]>(); // cada linha: 5 inteiros (row por reel)

        private void Reset()
        {
            // 25 linhas padrão para 5x3 (rows 0-top,1-mid,2-bottom)
            paylines = new List<int[]>
            {
                new[]{1,1,1,1,1}, // linha 1 (centro)
                new[]{0,0,0,0,0}, // topo
                new[]{2,2,2,2,2}, // base
                new[]{0,1,2,1,0},
                new[]{2,1,0,1,2},
                new[]{0,0,1,0,0},
                new[]{2,2,1,2,2},
                new[]{1,0,1,2,1},
                new[]{1,2,1,0,1},
                new[]{0,1,1,1,0},
                new[]{2,1,1,1,2},
                new[]{0,1,0,1,0},
                new[]{2,1,2,1,2},
                new[]{1,0,0,0,1},
                new[]{1,2,2,2,1},
                new[]{0,2,0,2,0},
                new[]{2,0,2,0,2},
                new[]{0,2,1,2,0},
                new[]{2,0,1,0,2},
                new[]{1,1,0,1,1},
                new[]{1,1,2,1,1},
                new[]{0,1,2,2,2},
                new[]{2,1,0,0,0},
                new[]{0,0,1,2,2},
                new[]{2,2,1,0,0},
            };
        }

        // grid[reel][row]
        public int EvaluateGrid(SymbolType[][] grid, System.Func<SymbolType, int, int> getPayout, System.Func<SymbolType, bool> isWild, out List<WinInfo> wins)
        {
            wins = new List<WinInfo>();
            int total = 0;

            foreach (var line in paylines)
            {
                // coleta símbolos da linha
                var first = grid[0][line[0]];
                int count = 1;
                SymbolType refSymbol = first;

                // Se o primeiro é wild, precisamos olhar o primeiro não-wild
                if (isWild(refSymbol))
                {
                    // busca refSymbol ao avançar
                    for (int r = 1; r < 5; r++)
                    {
                        var s = grid[r][line[r]];
                        if (!isWild(s)) { refSymbol = s; break; }
                    }
                }

                var positions = new List<Vector2Int> { new Vector2Int(0, line[0]) };

                for (int reel = 1; reel < 5; reel++)
                {
                    var s = grid[reel][line[reel]];
                    if (s.Equals(refSymbol) || isWild(s) || (isWild(refSymbol) && !s.Equals(SymbolType.Scatter)))
                    {
                        count++;
                        positions.Add(new Vector2Int(reel, line[reel]));
                    }
                    else break;
                }

                if (count >= 3 && refSymbol != SymbolType.Scatter && refSymbol != SymbolType.BonusCoin)
                {
                    int pay = getPayout(refSymbol, count);
                    if (pay > 0)
                    {
                        total += pay;
                        wins.Add(new WinInfo{ symbol=refSymbol, count=count, payout=pay, positions=positions});
                    }
                }
            }
            return total;
        }
    }
}
