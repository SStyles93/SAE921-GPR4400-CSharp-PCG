using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

namespace Player
{
    public class Attack : MonoBehaviour
    {
        [Tooltip("AttackDamage sent to the enemyStat")]
        [SerializeField] private float _attackDamage = 5.0f;
        
        private float _pushPower;

        public float PushPower { get => _pushPower; set => _pushPower = value; }

        /// <summary>
        /// Use the collider (on the weapon) to launch attack
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                //Send enemy in opposite direction from player
                Vector2 forceDirection = collision.gameObject.transform.position -
                    gameObject.transform.position;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(forceDirection * _pushPower, ForceMode2D.Impulse);

                collision.gameObject.GetComponent<EnemyStats>().TakeDamage(_attackDamage);
            }
        }
    }
}
