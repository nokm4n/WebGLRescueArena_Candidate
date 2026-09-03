using UnityEngine;

namespace WebGLRescueArena
{
    public sealed class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private int damage = 8;
        [SerializeField] private float attackRange = 1.35f;
        [SerializeField] private float attackCooldown = 0.9f;
        private float nextAttack;

        private PlayerHealth _health;

        public void Init(Transform target)
        {
            _health = target.GetComponent<PlayerHealth>();
        }

        public void Tick(float distance)
        {
            if (_health == null || Time.time < nextAttack || distance > attackRange) return;
            nextAttack = Time.time + attackCooldown;
            _health.TakeDamage(damage);
        }
    }
}
