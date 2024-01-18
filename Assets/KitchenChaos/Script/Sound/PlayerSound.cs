using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlayerSound : MonoBehaviour
    {
        private const float FOOTSTEP_TIMER_MAX = 0.1f;
        private const float FOOTSTEP_TIMER_MIN = 0.0f;

        [Header("External Ref")]
        [SerializeField] private PlayerController _playerController;

        [Header("Internal Ref")]
        [SerializeField] private AudioSource _audioSrc;

        private float _footstepTimer;

        private void Update()
        {
            _footstepTimer += Time.deltaTime;
            if (_footstepTimer >= FOOTSTEP_TIMER_MAX)
            {
                _footstepTimer = FOOTSTEP_TIMER_MIN;
                if (_playerController.IsMoving)
                {
                    _audioSrc.PlayOneShot(Bootstrap.Instance.SFXMgr.GetRandomFootStepAudioClip());
                }
            }
        }
    }
}
