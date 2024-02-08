using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlayerSound : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private PlayerSFXCfg _config;

        [Header("External Ref")]
        [SerializeField] private PlayerController _playerController;

        [Header("Internal Ref")]
        [SerializeField] private AudioSource _audioSrc;

        private float _footstepTimer;

        private void Update()
        {
            _footstepTimer += Time.deltaTime;
            if (_footstepTimer >= _config.FootstepTimerMax)
            {
                _footstepTimer = _config.FootstepTimerMin;
                if (_playerController.IsMoving)
                {
                    _audioSrc.PlayOneShot(Bootstrap.Instance.SFXMgr.GetRandomFootStepAudioClip());
                }
            }
        }
    }
}
