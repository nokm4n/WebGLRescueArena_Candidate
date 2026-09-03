using UnityEngine;

namespace WebGLRescueArena
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class Projectile : MonoBehaviour
    {
        [SerializeField] private float lifetime = 2.5f;
        [SerializeField] private GameObject fallbackImpactEffect;
        [SerializeField] private Rigidbody body;
        private int damage;

        private void Awake() 
        { 
            //body = GetComponent<Rigidbody>(); 
            Destroy(gameObject, lifetime); 
        }
       
        public void Launch(float speed, int damageValue) 
        { 
            damage = damageValue; 
            body.linearVelocity = transform.forward * speed; 
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(damage);
            }
            GameObject impact = Resources.Load<GameObject>("Effects/Impact") ?? fallbackImpactEffect;
            if (impact != null) Instantiate(impact, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
