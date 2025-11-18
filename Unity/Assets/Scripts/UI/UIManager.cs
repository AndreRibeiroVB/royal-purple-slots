// UIManager.cs
// HUD e integração mínima (placeholders com Debug.Log)
using UnityEngine;

namespace Slots.UI
{
    public class UIManager : MonoBehaviour
    {
        public float balance = 1000f;
        public float bet = 1f;

        public void SetBalance(float v) { balance = v; Debug.Log($"BALANCE: {balance}"); }
        public void SetBet(float v) { bet = Mathf.Max(0.1f, v); Debug.Log($"BET: {bet}"); }
        public void AddWin(float amount) { balance += amount; Debug.Log($"WIN +{amount} => BAL {balance}"); }
        public void DeductBet() { balance -= bet; Debug.Log($"BET -{bet} => BAL {balance}"); }

        public void ShowSmallWin() => Debug.Log("SmallWin");
        public void ShowBigWin() => Debug.Log("BigWin!!!");
        public void ShowBonusTransition() => Debug.Log("Bonus Transition");
        public void ShowFreeSpinsUI(bool on) => Debug.Log("FreeSpins UI: " + on);
    }
}
