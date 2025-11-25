// ParticleWinSystem.cs
// Sistema de partículas para vitórias com diferentes intensidades
using System.Collections.Generic;
using UnityEngine;

namespace Slots.Anim
{
    public class ParticleWinSystem : MonoBehaviour
    {
        [Header("Particle Prefabs")]
        public GameObject smallWinParticle;
        public GameObject mediumWinParticle;
        public GameObject bigWinParticle;
        public GameObject coinBurstPrefab;
        public GameObject starBurstPrefab;

        [Header("Emission Settings")]
        public int smallWinCount = 20;
        public int mediumWinCount = 50;
        public int bigWinCount = 100;
        public float particleLifetime = 2f;
        public float burstRadius = 3f;

        private List<GameObject> activeParticles = new List<GameObject>();

        /// <summary>
        /// Emite partículas baseado no valor da vitória
        /// </summary>
        public void EmitWinParticles(float winAmount, Vector3 position)
        {
            GameObject prefab;
            int count;

            if (winAmount < 10f)
            {
                prefab = smallWinParticle;
                count = smallWinCount;
            }
            else if (winAmount < 50f)
            {
                prefab = mediumWinParticle;
                count = mediumWinCount;
            }
            else
            {
                prefab = bigWinParticle;
                count = bigWinCount;
            }

            if (prefab == null) return;

            for (int i = 0; i < count; i++)
            {
                Vector3 randomOffset = Random.insideUnitSphere * burstRadius;
                randomOffset.z = 0f; // Manter em 2D

                GameObject particle = Instantiate(prefab, position + randomOffset, Quaternion.identity, transform);
                activeParticles.Add(particle);
                
                Destroy(particle, particleLifetime);
            }

            // Limpar referências depois do tempo de vida
            StartCoroutine(CleanupParticles());
        }

        /// <summary>
        /// Emite burst de moedas em uma posição específica
        /// </summary>
        public void EmitCoinBurst(Vector3 position, int coinCount = 15)
        {
            if (coinBurstPrefab == null) return;

            for (int i = 0; i < coinCount; i++)
            {
                float angle = (360f / coinCount) * i;
                Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
                Vector3 spawnPos = position + direction * 0.5f;

                GameObject coin = Instantiate(coinBurstPrefab, spawnPos, Quaternion.identity, transform);
                
                // Adicionar física para o burst
                Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(direction * Random.Range(300f, 500f));
                    rb.AddTorque(Random.Range(-100f, 100f));
                }

                Destroy(coin, particleLifetime);
            }
        }

        /// <summary>
        /// Emite burst de estrelas para Scatter wins
        /// </summary>
        public void EmitStarBurst(Vector3 position)
        {
            if (starBurstPrefab == null) return;

            for (int i = 0; i < 8; i++)
            {
                float angle = (360f / 8f) * i;
                Quaternion rotation = Quaternion.Euler(0, 0, angle);

                GameObject star = Instantiate(starBurstPrefab, position, rotation, transform);
                Destroy(star, particleLifetime);
            }
        }

        /// <summary>
        /// Emite partículas ao longo de uma linha de pagamento
        /// </summary>
        public void EmitPaylineTrail(List<Vector2Int> positions, Color color)
        {
            if (positions == null || positions.Count < 2) return;

            for (int i = 0; i < positions.Count - 1; i++)
            {
                Vector3 start = PositionToWorld(positions[i]);
                Vector3 end = PositionToWorld(positions[i + 1]);

                StartCoroutine(DrawParticleTrail(start, end, color));
            }
        }

        private System.Collections.IEnumerator DrawParticleTrail(Vector3 start, Vector3 end, Color color)
        {
            float duration = 0.5f;
            float elapsed = 0f;
            int particlesPerSegment = 10;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;

                Vector3 position = Vector3.Lerp(start, end, t);
                
                if (smallWinParticle != null)
                {
                    GameObject particle = Instantiate(smallWinParticle, position, Quaternion.identity, transform);
                    
                    // Aplicar cor se tiver SpriteRenderer
                    SpriteRenderer sr = particle.GetComponent<SpriteRenderer>();
                    if (sr != null) sr.color = color;

                    Destroy(particle, 0.5f);
                }

                yield return new WaitForSeconds(0.05f);
            }
        }

        private Vector3 PositionToWorld(Vector2Int gridPos)
        {
            // Converter posição da grid (reel, row) para posição world
            // Ajustar estes valores baseado no seu layout
            float x = gridPos.x * 2f - 4f; // 5 reels centrados
            float y = (1 - gridPos.y) * 2f; // 3 rows (top=0, mid=1, bot=2)
            return new Vector3(x, y, 0);
        }

        private System.Collections.IEnumerator CleanupParticles()
        {
            yield return new WaitForSeconds(particleLifetime);
            activeParticles.RemoveAll(p => p == null);
        }

        /// <summary>
        /// Limpa todas as partículas ativas imediatamente
        /// </summary>
        public void ClearAllParticles()
        {
            foreach (var particle in activeParticles)
            {
                if (particle != null) Destroy(particle);
            }
            activeParticles.Clear();
        }
    }
}
