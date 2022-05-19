using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        //Reference Scripts
        private PlayerController _playerController;
        private PlayerStats _playerStats;
        //Reference Components
        private Rigidbody2D _rb;

        private bool _canMove = true;
        public bool CanMove { get => _canMove; set => _canMove = value; }

        void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _playerStats = GetComponent<PlayerStats>();
            
            //Player Components
            _rb = GetComponent<Rigidbody2D>();

            _playerController.GameState += MovePlayer;
        }
        private void FixedUpdate()
        {
            //Movement
            if (_canMove)
            {
                if((Vector3)_playerController.Movement != Vector3.zero)
                {
                    _rb.MovePosition(transform.position + (Vector3)_playerController.Movement * _playerStats.Speed * Time.fixedDeltaTime);
                    
                }
            }
        }

        /// <summary>
        /// Stops the movement of the player when the game is not running
        /// </summary>
        /// <param name="state">The state in which we want the player to be</param>
        private void MovePlayer(bool state)
        {
            _canMove = state;
        }
    }
}
