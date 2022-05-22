using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerActions : MonoBehaviour
    {
        //Reference Scripts
        private PlayerController _playerController;
        private PlayerVisuals _playerVisuals;
        private PlayerStats _playerStats;

        //Reference GameObjects
        [Header("Player's body parts")]
        [SerializeField] private GameObject _cameraTarget;
        [SerializeField] private GameObject _aim;
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Attack _attack;


        //List of bools used for Actions
        [Header("Action's variables")]
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _aimCorrection;
        [SerializeField] private float _action1CoolDownTime = 1.0f;
        private float _action1CoolDown;
        [SerializeField] private float _action2CoolDownTime = 1.0f;
        private float _action2CoolDown;

        private bool _canHit = true;
        private bool _canShoot = true;
        private bool _canAttack = true;
        private bool _isInCombat = true;      
        
        private GameObject _currentProjectile;

        Vector3 currentAimPos;

        //Properties
        public bool CanHit { get => _canHit; set => _canHit = value; }
        public GameObject Aim { get => _aim; private set => _aim = value; }
        public bool IsInCombat { get => _isInCombat; set => _isInCombat = value; }

        void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _playerVisuals = GetComponentInChildren<PlayerVisuals>();
            _playerStats = GetComponent<PlayerStats>();
        }
        private void Start()
        {
            _aim.transform.localPosition = new Vector3(0.0f, -0.5f, 0.0f);
        }

        void Update()
        {
            PlayerLook();

            if (_canHit)
            {
                ActionCheck();
            }
        }

        /// <summary>
        /// Updates the player look when out of combat
        /// </summary>
        private void PlayerLook()
        {
            Vector3 currentAimPos = _aim.transform.localPosition;
            Vector2 movement = _playerController.Movement;
            if (_playerController.Movement != Vector2.zero)
            {
                _cameraTarget.transform.localPosition = new Vector3(movement.x, movement.y, 0.0f);
                _aim.transform.localPosition = new Vector3(movement.x, movement.y, 0.0f);
                //_cameraTarget.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                _cameraTarget.transform.localPosition = Vector3.zero;
                //_cameraTarget.GetComponent<SpriteRenderer>().enabled = false;
                _aim.transform.localPosition = currentAimPos;
            }
        }

        /// <summary>
        /// Activates the player's actions
        /// </summary>
        private void ActionCheck()
        {
            //Attack
            if (_playerController.Action1 && _canAttack)
            {
                _canAttack = false;
                Attack();
            }
            else if (!_canAttack)
            {
                _action1CoolDown -= Time.deltaTime;
                if (_action1CoolDown <= 0.0f)
                {
                    _action1CoolDown = _action1CoolDownTime;
                    _canAttack = true;
                }
            }
            
            //Projectile
            if (_playerController.Action2 && _canShoot)
            {
                InstantiateProjectile();
                _canShoot = false;

            }
            else if (!_canShoot)
            {
                _action2CoolDown -= Time.deltaTime;
                if (_action2CoolDown <= 0.0f)
                {
                    _action2CoolDown = _action2CoolDownTime;
                    _canShoot = true;
                }
            }
        }

        /// <summary>
        /// Instantiates a projectile
        /// </summary>
        private void InstantiateProjectile()
        {
            Vector3 instantiationPos = _aim.transform.position;

            // Instantiates the projectile and gives it the necessary data
            _currentProjectile = Instantiate(_projectilePrefab, instantiationPos, Quaternion.identity);
            //Projectile projectile = _currentProjectile.GetComponent<Projectile>();
            //projectile.ArrowDirection = _aim.transform.localPosition;
            //projectile.Damage *= _playerStats.Damage;
        }

        /// <summary>
        /// Launches the Attack
        /// </summary>
        private void Attack()
        {
            //Launch attack
            _playerVisuals.StartAttack();

            //Sets the pushing power of the attack
            _attack.GetComponent<Attack>().PushPower = _playerStats.PushPower;
        }
    }

}
