using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        //Reference Scripts
        private PlayerController _playerController;
        private PlayerMovement _playerMovement;
        private PlayerActions _playerActions;
        private PlayerAudio _playerAudio;
        private PlayerVisuals _playerVisuals;

        private float _health;
        
        [SerializeField] private bool _isInvicible = false;

        //Properties
        public bool IsInvicible { get => _isInvicible; set => _isInvicible = value; }

        private void Awake()
        {
            //_pushPower = _statSO.basicPushPower + (_statSO.basicPushPower * _statSO.pushPowerPercentage / 100.0f);
            //_armDamage = _armDamage + (_statSO.basicArmDamage * _statSO.armDamagePercentage / 100.0f);
            //_maxHealth = _statSO.basicHealth + (_statSO.basicHealth * _statSO.healthPercentage / 20.0f);
            //_speed = _statSO.basicSpeed + (_statSO.basicSpeed * _statSO.speedPercentage / 100.0f);

            //Get player components
            _playerController = GetComponent<PlayerController>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerActions = GetComponent<PlayerActions>();
            _playerAudio = GetComponent<PlayerAudio>();
            //Get player's body components
            _playerVisuals = GetComponentInChildren<PlayerVisuals>();

            _playerController.GameState += PauseStats;

        }

        private void Update()
        {
            if (_health <= 0.0f)
            {
                Die();
            }
        }

        /// <summary>
        /// Applies death to the player
        /// </summary>
        private void Die()
        {
            DisablePlayersActions();
            //ResetLife();
        }

        /// <summary>
        /// Pauses the player Stats when the game is not running
        /// </summary>
        /// <param name="state">State of game Play(true)/Pause(false)</param>
        private void PauseStats(bool state)
        {
            _isInvicible = !state;
            _playerActions.enabled = state;
        }

        /// <summary>
        /// Lowers health according to the damage
        /// </summary>
        /// <param name="damage">The damage to substract to health</param>
        public void TakeDamage(float damage)
        {
            if (!_isInvicible)
                _health -= damage;
            _playerVisuals.HitEffect();
            _playerAudio.PlayHit();
        }

        /// <summary>
        /// Disables all actions from player and makes him invulerable
        /// </summary>
        public void DisablePlayersActions()
        {
            IsInvicible = true;
            _playerActions.CanHit = false;
            _playerMovement.CanMove = false;
            _playerVisuals.Animator.speed = 0;
        }

        /// <summary>
        /// Resets the player's health
        /// </summary>
        public void ResetLife()
        {
            //Reset player's health
            //_health = //max value
        }
    }
}
