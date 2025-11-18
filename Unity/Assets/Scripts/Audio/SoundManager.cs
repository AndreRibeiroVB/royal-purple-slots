// SoundManager.cs
// Eventos sonoros (placeholder com AudioSource)
using UnityEngine;

namespace Slots.Audio
{
    public class SoundManager : MonoBehaviour
    {
        public AudioSource sfx;
        public AudioClip spinStart, spinStop, click, smallWin, mediumWin, bigWin,
            coinCollect, bonusTrigger, freeStart, freeEnd, bonusLoop, ambientLoop, uiOpen, uiClose;

        public void Play(AudioClip clip)
        {
            if (clip && sfx) sfx.PlayOneShot(clip);
        }

        public void SpinStart() => Play(spinStart);
        public void SpinStop() => Play(spinStop);
        public void Click() => Play(click);
        public void SmallWin() => Play(smallWin);
        public void MediumWin() => Play(mediumWin);
        public void BigWin() => Play(bigWin);
        public void CoinCollect() => Play(coinCollect);
        public void BonusTrigger() => Play(bonusTrigger);
        public void FreeStart() => Play(freeStart);
        public void FreeEnd() => Play(freeEnd);
    }
}
