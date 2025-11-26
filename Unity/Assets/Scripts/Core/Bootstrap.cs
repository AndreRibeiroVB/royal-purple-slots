// Bootstrap.cs
// Componente para montar rapidamente a cena: adicione a um GameObject vazio
using UnityEngine;
using Slots.Config;
using Slots.Services;
using Slots.Slots;
using Slots.Bonus;
using Slots.UI;
using Slots.Anim;

namespace Slots.Core
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            var gs = FindOrAdd<GameSettings>();
            var rng = FindOrAdd<RNGService>();
            var sym = FindOrAdd<SymbolManager>();
            var pay = FindOrAdd<PaylinesManager>();
            var ui = FindOrAdd<UIManager>();
            var bonus = FindOrAdd<BonusPotController>();
            var free = FindOrAdd<FreeSpinsController>();
            var anim = FindOrAdd<AnimationManager>();

            var spinGo = new GameObject("SpinManager");
            var spin = spinGo.AddComponent<SpinManager>();
            spin.rng = rng; spin.symbols = sym; spin.paylines = pay; spin.ui = ui; spin.bonus = bonus; spin.freeSpins = free; spin.settings = gs; spin.anim = anim;

            // criar 5 reels
            spin.reels = new ReelController[5];
            for (int i = 0; i < 5; i++)
            {
                var rgo = new GameObject($"Reel_{i}");
                var rc = rgo.AddComponent<ReelController>();
                rc.reelIndex = i;
                spin.reels[i] = rc;
            }

            Debug.Log("Bootstrap completo. Use SpinManager.Spin() para iniciar.");
        }

        private T FindOrAdd<T>() where T : Component
        {
            var c = FindAnyObjectByType<T>();
            if (!c) c = new GameObject(typeof(T).Name).AddComponent<T>();
            return c;
        }
    }
}
