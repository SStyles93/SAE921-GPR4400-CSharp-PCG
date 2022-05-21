using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //if (collision.gameObject.GetComponent<EnemyStats>())
            //{
            //    //Send enemy in opposite direction from player
            //    Vector2 forceDirection = collision.gameObject.transform.position -
            //        gameObject.transform.position;
            //    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(forceDirection * (_pushPower * 100.0f), ForceMode2D.Impulse);

            //    collision.gameObject.GetComponent<EnemyStats>().TakeDamage(_attackDamage);
            //}
        }
    }
}
