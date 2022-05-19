using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerActions : MonoBehaviour
    {
        //Reference Scripts
        private PlayerController _playerController;

        //Reference GameObjects
        [Header("Player's body parts")]
        [SerializeField] private GameObject mouseTarget;
        [SerializeField] private GameObject _cameraTarget;
        [SerializeField] private GameObject _aim;
        
        //List of bools used for Actions
        [Header("Action's variables")]
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _aimCorrection;
        [SerializeField] private float _actionCoolDownTime = 1.0f;
        private bool _canHit = true;
        private bool _isInCombat = true;

        Vector3 currentAimPos;

        //Properties
        public bool CanHit { get => _canHit; set => _canHit = value; }
        //public bool[] CanThrow { get => _canThrow; set => _canThrow = value; }
        //public bool CanHeadbutt { get => _canHeadbutt; set => _canHeadbutt = value; }
        public GameObject Aim { get => _aim; private set => _aim = value; }
        public bool IsInCombat { get => _isInCombat; set => _isInCombat = value; }
        //public GameObject CurrentRightArm { get => currentRightArm; private set => currentRightArm = value; }
        //public GameObject CurrentLeftArm { get => currentLeftArm; private set => currentLeftArm = value; }

        void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }
        private void Start()
        {
            _aim.transform.localPosition = new Vector3(0.0f, -0.5f, 0.0f);
            //_chosenAbilityIdxR = _playerStatSO.rightArmType;
            //_chosenAbilityIdxL = _playerStatSO.leftArmType;
        }

        void Update()
        {
            if (_isInCombat)
            {
                PlayerCombatLook();
            }

            if (_canHit)
            {
                ActionCheck();
            }
        }

        /// <summary>
        /// Updates the player look direction
        /// </summary>
        private void PlayerCombatLook()
        {
            switch (_playerController.ControlScheme)
            {
                case "Gamepad":

                    //Switches from MouseTarget to GamepadTarget
                    mouseTarget.gameObject.SetActive(false);
                    _cameraTarget.GetComponent<SpriteRenderer>().enabled = true;

                    //Updates the Aim position according to the Gamepad input
                    Vector2 look = _playerController.Aim;
                    //Movement used for headbut aiming
                    Vector2 movement = _playerController.Movement;

                    if (look != Vector2.zero)
                    {
                        _cameraTarget.transform.localPosition = new Vector3(look.x, look.y, 0.0f);
                       currentAimPos = _aim.transform.localPosition = new Vector3(look.x, look.y, 0.0f);
                        _cameraTarget.GetComponent<SpriteRenderer>().enabled = true;
                    }

                    #region MoveToAim

                    //else if (_playerController.Movement != Vector2.zero)
                    //{
                    //    _cameraTarget.transform.localPosition = new Vector3(movement.x, movement.y, 0.0f);
                    //    _aim.transform.localPosition = new Vector3(movement.x, movement.y, 0.0f);
                    //    _cameraTarget.GetComponent<SpriteRenderer>().enabled = true;
                    //}

                    #endregion

                    else
                    {
                        _cameraTarget.transform.localPosition = Vector3.zero;
                        _cameraTarget.GetComponent<SpriteRenderer>().enabled = false;
                        _aim.transform.localPosition = currentAimPos;
                    }
                    break;
                case "Keyboard":

                    //Switches from GamepadTarget to MouseTarget
                    mouseTarget.gameObject.SetActive(true);
                    _cameraTarget.GetComponent<SpriteRenderer>().enabled = false;

                    //Updates the Aim position according to the Mouse position 
                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = 0.0f;
                    mouseTarget.transform.position = mousePos;
                    _aim.transform.localPosition =
                    _cameraTarget.transform.localPosition =
                        mouseTarget.transform.localPosition.normalized;
                    break;

                default:
                    mouseTarget.gameObject.SetActive(false);
                    break;
            }

            Vector3 correctedPos = _aim.transform.localPosition;
            correctedPos.x += correctedPos.x * -_aimCorrection;
            correctedPos.y += correctedPos.y * -_aimCorrection;
            _aim.transform.localPosition = correctedPos;
        }

        /// <summary>
        /// Updates the player look when out of combat
        /// </summary>
        private void PlayerPasiveLook()
        {
            Vector3 currentAimPos = _aim.transform.localPosition;
            Vector2 movement = _playerController.Movement;
            if (_playerController.Movement != Vector2.zero)
            {
                _cameraTarget.transform.localPosition = new Vector3(movement.x, movement.y, 0.0f);
                _aim.transform.localPosition = new Vector3(movement.x, movement.y, 0.0f);
                _cameraTarget.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                _cameraTarget.transform.localPosition = Vector3.zero;
                _cameraTarget.GetComponent<SpriteRenderer>().enabled = false;
                _aim.transform.localPosition = currentAimPos;
            }
        }

        /// <summary>
        /// Activates the player's actions
        /// </summary>
        private void ActionCheck()
        {
            ////RightArm Throw
            //if (_playerController.ArmR && _canThrow[(int)BODYPART.RIGHTARM])
            //{
            //    if(_playerController.ControlScheme == "Gamepad" && _playerController.Aim == Vector2.zero)
            //    {
            //        return;
            //    }

            //    EnablePlayersArm(BODYPART.RIGHTARM, false);
            //    InstantiateArm(BODYPART.RIGHTARM);
            //    _canThrow[(int)BODYPART.RIGHTARM] = false;

            //}

            ////LeftArm Throw
            //if (_playerController.ArmL && _canThrow[(int)BODYPART.LEFTARM])
            //{
            //    if (_playerController.ControlScheme == "Gamepad" && _playerController.Aim == Vector2.zero)
            //    {
            //        return;
            //    }

            //    EnablePlayersArm(BODYPART.LEFTARM, false);
            //    InstantiateArm(BODYPART.LEFTARM);
            //    _canThrow[(int)BODYPART.LEFTARM] = false;
            //}

            ////Headbutt
            //if (_playerController.Head && _canHeadbutt)
            //{
            //    _canHeadbutt = false;
            //    Headbutt();
            //}
            //else if (!_canHeadbutt)
            //{
            //    _headbuttCoolDown -= Time.deltaTime;
            //    if (_headbuttCoolDown <= 0.0f)
            //    {
            //        _headbuttCoolDown = _headbuttCoolDownTime;
            //        _canHeadbutt = true;
            //    }
            //}
        }

        ///// <summary>
        ///// Enables or disables Arms
        ///// </summary>
        ///// <param name="armSide">Enum use to indicate which arms to interact with</param>
        ///// <param name="enable">Bool to enable or disable</param>
        //public void EnablePlayersArm(BODYPART armSide, bool enable)
        //{
        //    _arms[(int)armSide].SetActive(enable);
        //    _canThrow[(int)armSide] = true;

        //}

        ///// <summary>
        ///// Instantiates an arm
        ///// </summary>
        ///// <param name="armSide">Define which arm to instantiate</param>
        //private void InstantiateArm(BODYPART armSide)
        //{
        //    Vector3 instantiationPos = _aim.transform.position;

        //    if (armSide == BODYPART.RIGHTARM)
        //    {
        //        //will have to adapt to ability chosen
        //        currentRightArm = Instantiate(_rightArms[_chosenAbilityIdxR].gameObject, instantiationPos, Quaternion.identity);
        //        Arm arm = currentRightArm.GetComponent<Arm>();
        //        arm.ArmDirection = _aim.transform.localPosition;
        //        arm.Damage *= _playerStats.ArmDamage;
        //        //ThrowPosition used for BommerangArm
        //        arm.ThrowPosition = transform.position;
        //        //PushPower used for Explosive
        //        arm.PushPower *= _playerStats.PushPower;
        //        arm.Player = gameObject;
        //    }
        //    if (armSide == BODYPART.LEFTARM)
        //    {
        //        //will have to adapt to ability chosen
        //        currentLeftArm = Instantiate(_leftArms[_chosenAbilityIdxL].gameObject, instantiationPos, Quaternion.identity);
        //        Arm arm = currentLeftArm.GetComponent<Arm>();
        //        arm.ArmDirection = _aim.transform.localPosition;
        //        arm.Damage *= _playerStats.ArmDamage;
        //        //ThrowPosition used for BommerangArm
        //        arm.ThrowPosition = transform.position;
        //        //PushPower used for Explosive
        //        arm.PushPower *= _playerStats.PushPower;
        //        arm.Player = gameObject;
        //    }
        //}

        ///// <summary>
        ///// Launches the Headbutt
        ///// </summary>
        //private void Headbutt()
        //{
        //    _playerVisuals.StartHeadbutt();
        //    _head.GetComponent<Headbutt>().PushPower = _playerStats.PushPower;
        //}
    }

}
