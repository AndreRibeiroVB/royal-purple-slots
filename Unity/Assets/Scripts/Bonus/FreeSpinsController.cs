// FreeSpinsController.cs
// Controle de Free Spins com multiplicador progressivo
using UnityEngine;

namespace Slots.Bonus
{
    public class FreeSpinsController : MonoBehaviour
    {
        public bool inFreeSpins;
        public int spinsLeft;
        public int startSpins = 10;
        public int multiplier = 1;
        public int increaseEveryWins = 2;
        private int winsSinceIncrease = 0;

        public System.Action OnFreeStart;
        public System.Action OnFreeEnd;

        public void StartFreeSpins(int count)
        {
            inFreeSpins = true;
            spinsLeft = count > 0 ? count : startSpins;
            multiplier = 1;
            winsSinceIncrease = 0;
            OnFreeStart?.Invoke();
        }

        public void RegisterSpinResult(bool hadWin)
        {
            if (!inFreeSpins) return;
            spinsLeft--;
            if (hadWin)
            {
                winsSinceIncrease++;
                if (winsSinceIncrease >= increaseEveryWins)
                {
                    multiplier++;
                    winsSinceIncrease = 0;
                }
            }
            if (spinsLeft <= 0) EndFreeSpins();
        }

        public void EndFreeSpins()
        {
            inFreeSpins = false;
            OnFreeEnd?.Invoke();
        }
    }
}
