using UnityEngine;

namespace WebGLRescueArena
{
    public sealed class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private PlayerInputReader input;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireRate = 0.12f;
        [SerializeField] private float projectileSpeed = 18f;
        [SerializeField] private int damage = 10;
        private float nextShotTime;
        private bool _isActive = true;

        private void Start()
        {
            GameEvents.PlayerDied += StopGame;
            _isActive = true;
        }

        private void OnDisable()
        {
            GameEvents.PlayerDied -= StopGame;
        }

        private void Update()
        {
            if (!_isActive || !input.FireHeld || Time.time < nextShotTime) return;

            nextShotTime = Time.time + fireRate;
            Projectile projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.Launch(projectileSpeed, damage);
        }

        private void StopGame()
        {
            _isActive = false;
        }
    }
}
