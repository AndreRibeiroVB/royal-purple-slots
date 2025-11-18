// SymbolManager.cs
// Define símbolos, pesos por volatilidade e tabela de pagamentos
using System.Collections.Generic;
using UnityEngine;
using Slots.Config;

namespace Slots.Slots
{
    public enum SymbolType
    {
        A, K, Q, J,
        PurpleGem, WhiteCoin, Mystic,
        PurpleDiamond, WhiteCrown,
        Wild, Scatter, BonusCoin
    }

    [System.Serializable]
    public class SymbolPayout
    {
        public SymbolType symbol;
        // Índices: 3,4,5 of-a-kind (0,1,2)
        public int[] pays = new int[3];
    }

    public class SymbolManager : MonoBehaviour
    {
        [Header("Payouts por linha (multiplicador do bet)")]
        public List<SymbolPayout> payouts = new List<SymbolPayout>();

        private Dictionary<SymbolType, int[]> payoutMap;

        private void Reset()
        {
            payouts = new List<SymbolPayout>
            {
                new SymbolPayout{ symbol = SymbolType.J, pays = new[]{1,2,5}},
                new SymbolPayout{ symbol = SymbolType.Q, pays = new[]{1,2,5}},
                new SymbolPayout{ symbol = SymbolType.K, pays = new[]{2,5,10}},
                new SymbolPayout{ symbol = SymbolType.A, pays = new[]{2,5,10}},
                new SymbolPayout{ symbol = SymbolType.PurpleGem, pays = new[]{5,10,20}},
                new SymbolPayout{ symbol = SymbolType.WhiteCoin, pays = new[]{5,10,20}},
                new SymbolPayout{ symbol = SymbolType.Mystic, pays = new[]{10,15,20}},
                new SymbolPayout{ symbol = SymbolType.PurpleDiamond, pays = new[]{25,50,100}},
                new SymbolPayout{ symbol = SymbolType.WhiteCrown, pays = new[]{40,80,150}},
                new SymbolPayout{ symbol = SymbolType.Wild, pays = new[]{0,0,0}},
                new SymbolPayout{ symbol = SymbolType.Scatter, pays = new[]{0,0,0}},
                new SymbolPayout{ symbol = SymbolType.BonusCoin, pays = new[]{0,0,0}},
            };
        }

        private void Awake()
        {
            payoutMap = new Dictionary<SymbolType, int[]>();
            foreach (var p in payouts)
                payoutMap[p.symbol] = p.pays;
        }

        public int GetLinePayout(SymbolType symbol, int count)
        {
            if (!payoutMap.ContainsKey(symbol)) return 0;
            if (count < 3) return 0;
            int idx = Mathf.Clamp(count - 3, 0, 2);
            return payoutMap[symbol][idx];
        }

        public bool IsWild(SymbolType s) => s == SymbolType.Wild;
        public bool IsScatter(SymbolType s) => s == SymbolType.Scatter;
        public bool IsBonus(SymbolType s) => s == SymbolType.BonusCoin;

        // Pesos simplificados por volatilidade (probabilidades relativas, somatório livre)
        public Dictionary<VolatilityMode, Dictionary<SymbolType, int>> GetWeights()
        {
            return new Dictionary<VolatilityMode, Dictionary<SymbolType, int>>
            {
                [VolatilityMode.Low] = new Dictionary<SymbolType, int>
                {
                    {SymbolType.J, 90}, {SymbolType.Q, 90}, {SymbolType.K, 80}, {SymbolType.A, 80},
                    {SymbolType.PurpleGem, 60}, {SymbolType.WhiteCoin, 60}, {SymbolType.Mystic, 50},
                    {SymbolType.PurpleDiamond, 25}, {SymbolType.WhiteCrown, 15},
                    {SymbolType.Wild, 20}, {SymbolType.Scatter, 10}, {SymbolType.BonusCoin, 12}
                },
                [VolatilityMode.Medium] = new Dictionary<SymbolType, int>
                {
                    {SymbolType.J, 80}, {SymbolType.Q, 80}, {SymbolType.K, 70}, {SymbolType.A, 70},
                    {SymbolType.PurpleGem, 55}, {SymbolType.WhiteCoin, 55}, {SymbolType.Mystic, 45},
                    {SymbolType.PurpleDiamond, 30}, {SymbolType.WhiteCrown, 20},
                    {SymbolType.Wild, 25}, {SymbolType.Scatter, 12}, {SymbolType.BonusCoin, 14}
                },
                [VolatilityMode.High] = new Dictionary<SymbolType, int>
                {
                    {SymbolType.J, 70}, {SymbolType.Q, 70}, {SymbolType.K, 60}, {SymbolType.A, 60},
                    {SymbolType.PurpleGem, 45}, {SymbolType.WhiteCoin, 45}, {SymbolType.Mystic, 40},
                    {SymbolType.PurpleDiamond, 35}, {SymbolType.WhiteCrown, 25},
                    {SymbolType.Wild, 30}, {SymbolType.Scatter, 14}, {SymbolType.BonusCoin, 16}
                },
            };
        }
    }
}
