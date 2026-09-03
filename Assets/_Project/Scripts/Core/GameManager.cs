using Unity.VisualScripting;
using UnityEngine;

namespace WebGLRescueArena
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private HUDController hud;
        [SerializeField] private GameOverUI gameOverUI;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private bool stressMode;

        private SaveService _saveService;
        private SceneLoader _sceneLoader;
        private int score;
        private static int accumulatedScore;
        private float elapsedTime;
        private bool ended;
        public bool StressMode => stressMode;

        private void Awake() 
        { 
            if (stressMode) 
                enemySpawner.EnableStressMode(); 
        }

        private void OnEnable() 
        { 
            GameEvents.EnemyKilled += OnEnemyKilled; 
            GameEvents.PlayerDied += OnPlayerDied; 
        }

        private void OnDisable()
        {
            GameEvents.EnemyKilled -= OnEnemyKilled;
            GameEvents.PlayerDied -= OnPlayerDied;
        }

        private void Update()
        {
            if (ended) return;

            elapsedTime += Time.deltaTime;
            hud.Refresh(score, playerHealth.CurrentHealth, enemySpawner.ActiveEnemyCount, elapsedTime);
            if (Input.GetKeyDown(KeyCode.F8)) enemySpawner.EnableStressMode();
        }

        private void Start() 
        {
            _saveService = FindAnyObjectByType<SaveService>();
            _sceneLoader = FindAnyObjectByType<SceneLoader>();
            score = accumulatedScore; 
            gameOverUI.Hide(); 
            GameEvents.RaiseGameStarted(); 
        }

        private void OnEnemyKilled(int value) 
        { 
            accumulatedScore += value; 
            score = accumulatedScore; 
            GameEvents.RaiseScoreChanged(score); 
        }

        private void OnPlayerDied() 
        { 
            if (ended) return; 
            ended = true; 
            _saveService.SaveBestScore(score); 
            gameOverUI.Show(score, _saveService.BestScore); 
            GameEvents.RaiseGameEnded(); 
        }

        public void Restart() => _sceneLoader.RestartGame();
        public void ReturnToMenu() => _sceneLoader.LoadMainMenu();
    }
}
