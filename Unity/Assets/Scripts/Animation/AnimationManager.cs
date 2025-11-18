// AnimationManager.cs
// Pontos de extensão para animações (placeholder)
using UnityEngine;

namespace Slots.Anim
{
    public class AnimationManager : MonoBehaviour
    {
        public void HighlightWinPositions(System.Collections.Generic.List<UnityEngine.Vector2Int> positions)
        {
            Debug.Log($"Highlight {positions.Count} positions");
        }

        public void ShowBigWin(float amount)
        {
            Debug.Log($"BIG WIN POPUP: {amount}");
        }

        public void TransitionToBonus(bool toBonus)
        {
            Debug.Log("TransitionToBonus: " + toBonus);
        }
    }
}
