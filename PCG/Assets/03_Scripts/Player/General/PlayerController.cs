using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        //Reference Scripts
        private PlayerInput _playerInput;

        private string _controlScheme;

        //Movement Vectors
        private Vector2 _movement;
        private Vector2 _aim;

        //Action bools
        private bool _action1;
        private bool _action2;
        private bool _action3;

        //Game State bools
        public bool play = true;
        public delegate void Pause(bool isActive);
        public Pause GameState;

        //Repeating logic
        private bool _canRepeateActions = false;
        private bool _repeatingAction1 = false;
        private bool _repeatingAction2 = false;
        private bool _repeatingAction3 = false;
        private float _repeatingTimer = 0.1f;
        private float _repeatTimerAction1;
        private float _repeatTimerAction2;
        private float _repeatTimerAction3;

        //Properties
        public string ControlScheme { get => _controlScheme; set => _controlScheme = value; }
        public Vector2 Movement { get => _movement; set => _movement = value; }
        public Vector2 Aim { get => _aim; set => _aim = value; }
        public bool Action1 { get => _action1; set => _action1 = value; }
        public bool Action2 { get => _action2; set => _action2 = value; }
        public bool Action3 { get => _action3; set => _action3 = value; }
        public bool CanRepeateActions { get => _canRepeateActions; set => _canRepeateActions = value; }

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            _controlScheme = _playerInput.currentControlScheme;

            if (!_canRepeateActions)
            {
                //Prevents from repeating input HEAD
                if (_repeatingAction1)
                {
                    _repeatTimerAction1 += Time.deltaTime;
                }
                if (_repeatTimerAction1 >= _repeatingTimer)
                {
                    _action1 = true;
                    _repeatingAction1 = false;
                    _repeatTimerAction1 = 0.0f;
                }
                else
                {
                    _action1 = false;
                }

                //Prevents from repeating input ArmR
                if (_repeatingAction2)
                {
                    _repeatTimerAction2 += Time.deltaTime;
                }
                if (_repeatTimerAction2 >= _repeatingTimer)
                {
                    _action2 = true;
                    _repeatingAction2 = false;
                    _repeatTimerAction2 = 0.0f;
                }
                else
                {
                    _action2 = false;
                }

                //Prevents from repeating input ArmL
                if (_repeatingAction3)
                {
                    _repeatTimerAction3 += Time.deltaTime;
                }
                if (_repeatTimerAction3 >= _repeatingTimer)
                {
                    _action3 = true;
                    _repeatingAction3 = false;
                    _repeatTimerAction3 = 0.0f;
                }
                else
                {
                    _action3 = false;
                }
            }
        }

        public void OnMove(InputValue value)
        {
            _movement = value.Get<Vector2>();
        }
        public void OnAim(InputValue value)
        {
            switch (_controlScheme)
            {
                case "Keyboard":
                    break;
                case "Gamepad":
                    _aim = value.Get<Vector2>();
                    break;
                default:
                    break;
            }

        }
        public void OnHead(InputValue value)
        {
            if (!_canRepeateActions)
            {
                if (value.isPressed)
                    _repeatingAction1 = true;

            }
            else
            {
                _action1 = value.isPressed;
            }
        }
        public void OnArmR(InputValue value)
        {
            if (!_canRepeateActions)
            {
                if (value.isPressed)
                    _repeatingAction2 = true;

            }
            else
            {
                _action2 = value.isPressed;
            }
        }
        public void OnArmL(InputValue value)
        {
            if (!_canRepeateActions)
            {
                if (value.isPressed)
                    _repeatingAction3 = true;

            }
            else
            {
                _action3 = value.isPressed;
            }
        }

        public void OnPause(InputValue value)
        {
            play = !play;
            Time.timeScale = play ? 1.0f : 0.0f;
            GameState(play);
        }
    }
}
