using System.Collections;
using UnityEngine;

namespace WebGLRescueArena
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyController enemyPrefab;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private Transform enemyContainer;
        [SerializeField] private int startingEnemies = 12;
        [SerializeField] private int normalCap = 55;
        [SerializeField] private int stressCap = 140;
        [SerializeField] private float spawnInterval = 1f;

        private int cap;
        private bool _isActive = true;
        private IEnumerator _spawnCoroutine;
        public int ActiveEnemyCount => enemyContainer.childCount;
       
        private void Start() 
        {
            GameEvents.PlayerDied += StopGame;
            cap = normalCap;
            _spawnCoroutine = SpawnLoop();
            StartCoroutine(_spawnCoroutine);
            for (int index = 0; index < startingEnemies; index++) 
                SpawnEnemy(); 
        }

        private void OnDisable()
        {
            GameEvents.PlayerDied -= StopGame;
        }

        public void EnableStressMode() 
        { 
            cap = stressCap; 
            spawnInterval = 0.12f; 
        }

        private IEnumerator SpawnLoop()
        {
            while (_isActive) 
            { 
                if (ActiveEnemyCount < cap) 
                    SpawnEnemy(); 
                yield return new WaitForSeconds(spawnInterval); 
            }
        }
        private void SpawnEnemy()
        {
            Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, point.position, Quaternion.identity, enemyContainer);
        }

        private void StopGame()
        {
            _isActive = false;
            StopCoroutine(_spawnCoroutine);
        }
    }
}
