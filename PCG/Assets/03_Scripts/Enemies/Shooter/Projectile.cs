using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D),typeof(CircleCollider2D))]
    public class Projectile : MonoBehaviour
    {
        //Reference Components
        private Rigidbody2D _rb;

        //RibThrow direction
        private Vector3 _projectileDirection;

        //Rib Stats
        private float _speed = 1.0f;
        private float _damage = 1.0f;
        private bool _canMove = true;

        public float Speed { get => _speed; set => _speed = value; }
        public float Damage { get => _damage; set => _damage = value; }
        public bool CanMove { get => _canMove; set => _canMove = value; }
        public Vector3 ProjectileDirection { get => _projectileDirection; set => _projectileDirection = value; }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.gravityScale = 0.0f;

        }

        void Update()
        {
            Move();
        }

        private void Move()
        {
            if (_canMove)
            {
                _rb.constraints = RigidbodyConstraints2D.None;
                //Physic movement
                _rb.AddForce(_projectileDirection * _speed / 10.0f, ForceMode2D.Impulse);
                transform.Rotate(Vector3.back * Time.deltaTime * 1000f);
            }
            else
            {
                Destroy(gameObject);
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Uses the Trigger to hit
            if (!collision.gameObject.CompareTag("Enemy"))
            {
                _canMove = false;
                if (collision.gameObject.CompareTag("Player"))
                {
                    collision.GetComponent<PlayerStats>()?.TakeDamage(_damage);
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce((collision.gameObject.transform.position -
                    gameObject.transform.position) * 100.0f, ForceMode2D.Impulse);
                }
                
            }
        }
    }
}
