using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class ShooterAttack : MonoBehaviour
    {
        //Reference Scripts
        [SerializeField] private EnemyStats _enemyStats;
        [SerializeField] private EnemyRayCaster _enemyRayCaster;
        
        //Reference Prefab
        [SerializeField] private GameObject _projectilePrefab;
        
        //Variables
        [SerializeField] private float _damage = 1.0f;
        [SerializeField] private float _projectileSpeed = 0.5f;

        private void Awake()
        {
            _enemyStats = GetComponentInParent<EnemyStats>();
        }
        
        void Start()
        {
            _damage = _enemyStats.Damage;
        }

        public void LaunchRib()
        {
            GameObject currentProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
            Projectile projectile = currentProjectile.GetComponent<Projectile>();
            projectile.ProjectileDirection = _enemyRayCaster.Target.position - transform.position;
            projectile.Damage = _damage;
            projectile.Speed = _projectileSpeed;

        }
    }
}
