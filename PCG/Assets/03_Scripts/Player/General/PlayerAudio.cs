using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAudio : MonoBehaviour
    {
        //Reference Scripts
        private PlayerController _playerController;

        //Reference Components
        private AudioSource _audioSource;

        //Audio Clips
        [Header("Audio Clips")]
        [SerializeField] private AudioClip _footStepsClip;
        [SerializeField] private AudioClip _arrowShootClip;
        [SerializeField] private AudioClip[] _attackClips;
        [SerializeField] private AudioClip[] _hitClips;


        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            //FootSteps
            if (_playerController.Movement != Vector2.zero)
            {
                PlayClip(_footStepsClip);
            }
            //ArmThrow
            if (_playerController.Action3 || _playerController.Action2)
            {
                PlayClip(_arrowShootClip);
            }
            //HeadButt
            if (_playerController.Action1)
            {
                PlayClip(_attackClips[Random.Range(0, _attackClips.Length)]);
            }
        }

        /// <summary>
        /// Plays the Audio clip given to the method
        /// </summary>
        /// <param name="clip">The clip to play</param>
        private void PlayClip(AudioClip clip)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = clip;
                _audioSource.pitch = 1.0f;
                _audioSource.Play();
            }
        }

        /// <summary>
        /// Plays the hit audio effect
        /// </summary>
        public void PlayHit()
        {
            _audioSource.clip = _hitClips[Random.Range(0, _hitClips.Length)];
            _audioSource.pitch = 3.0f;
            _audioSource.Play();
        }
    }
}
