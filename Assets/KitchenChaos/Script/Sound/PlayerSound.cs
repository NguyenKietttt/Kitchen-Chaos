using System;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlayerSound : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private PlayerCfg _config;

        [Header("Internal Ref")]
        [SerializeField] private AudioSource _audioSrc;

        private float _footstepTimer;
        private bool _isPlayerMoving;

        private void OnEnable()
        {
            Bootstrap.Instance.EventMgr.PlayerMove += OnPlayerMove;
            Bootstrap.Instance.EventMgr.PlayerStop += OnPlayerStop;
        }

        private void Update()
        {
            _footstepTimer += Time.deltaTime;
            if (_footstepTimer >= _config.FootstepTimerMax)
            {
                _footstepTimer = _config.FootstepTimerMin;
                if (_isPlayerMoving)
                {
                    _audioSrc.PlayOneShot(Bootstrap.Instance.SFXMgr.GetRandomFootStepAudioClip());
                }
            }
        }

        private void OnPlayerMove()
        {
            _isPlayerMoving = true;
        }

        private void OnPlayerStop()
        {
            _isPlayerMoving = false;
        }
    }
}
