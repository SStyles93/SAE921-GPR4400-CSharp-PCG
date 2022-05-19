using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        //Reference Scripts
        private PlayerController _playerController;

        //Reference Components
        private Rigidbody2D _rb;

        private bool _canMove = true;
        public bool CanMove { get => _canMove; set => _canMove = value; }

        void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _rb = GetComponent<Rigidbody2D>();

            _playerController.GameState += MovePlayer;
        }
        private void FixedUpdate()
        {
            //Movement
            if (_canMove)
            {
                Vector3 movement = new Vector3(_playerController.Movement.x, _playerController.Movement.y, 0.0f);
                //transform.Translate(movement * _playerStats.Speed * Time.deltaTime);
                if(movement != Vector3.zero)
                {
                    _rb.MovePosition(transform.position + movement * Time.fixedDeltaTime);
                    
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
