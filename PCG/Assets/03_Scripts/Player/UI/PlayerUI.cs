using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerUI : MonoBehaviour
    {
        //Reference Scripts
        [Header("Player Scripts")]
        //[SerializeField] private PlayerInput _playerInput;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerStats _playerStats;

        //Reference Components
        [Header("Player Health UI")]
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Gradient _healthGradient;
        [SerializeField] private Image _healthFill;
        [SerializeField] private Image _actionBtnImage;

        [Header("Player Actions UI")]
        [SerializeField] private Color _blockedColor = Color.gray;
        private Color _normalColor = Color.white;

        //ScriptableObjects
        [SerializeField] private UIButtonsSO _uIButtonsSO;

        private void Awake()
        {
            _playerStats = GetComponentInParent<PlayerStats>();
            _playerController = GetComponentInParent<PlayerController>();

            _healthSlider = GetComponentInChildren<Slider>();
        }
        private void Start()
        {
            _healthSlider.maxValue = _playerStats.Health;
            _healthSlider.value = _playerStats.CurrentHealth;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateHealthBar();
            UpdateButtonUi();
            UpdateButtonsUiColor();
        }

        /// <summary>
        /// Updates the player's healthbar UI
        /// </summary>
        private void UpdateHealthBar()
        {
            _healthSlider.value = _playerStats.Health;
            _healthFill.color = _healthGradient.Evaluate(_healthSlider.value / _healthSlider.maxValue);
            
        }
        /// <summary>
        /// Updated the button UI according to PlayerController
        /// </summary>
        private void UpdateButtonsUiColor()
        {
            // If button pressed => blockedColor
            _actionBtnImage.color =
                _playerController.Action1 ? _blockedColor : _normalColor;
        }

        /// <summary>
        /// Updated the buttons UI to match the Controllers (ControlScheme)
        /// </summary>
        private void UpdateButtonUi()
        {
            switch (_playerController.ControlScheme)
            {
                case "Keyboard":

                    _actionBtnImage.sprite = _uIButtonsSO.keyboardSprite;
                    
                    break;
                case "Gamepad":

                    _actionBtnImage.sprite = _uIButtonsSO.gamepadSprite;
                    
                    break;
                default:
                    break;
            }

        }
    }
}
