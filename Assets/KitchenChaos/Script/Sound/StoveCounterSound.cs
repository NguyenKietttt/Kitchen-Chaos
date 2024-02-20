using UnityEngine;

namespace KitchenChaos
{
    public sealed class StoveCounterSound : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private StoveCounterSFXCfg _config;

        [Header("External Ref")]
        [SerializeField] private GameObject _stoveCounterObj;

        [Header("Internal Ref")]
        [SerializeField] private StoveCounter _stoveCounter;
        [SerializeField] private AudioSource _audioSrc;

        private float _warningSoundTimer;
        private bool _shouldPlayWarningSound;

        private void Start()
        {
            Bootstrap.Instance.EventMgr.ChangeStoveCounterState += OnStoveCounterState;
            Bootstrap.Instance.EventMgr.UpdateCounterProgress += OnCounterProgressChanged;
        }

        private void Update()
        {
            if (!_shouldPlayWarningSound)
            {
                _warningSoundTimer = _config.WarningTimerMax;
                return;
            }

            _warningSoundTimer -= Time.deltaTime;
            if (_warningSoundTimer <= _config.WarningTimerMin)
            {
                _warningSoundTimer = _config.WarningTimerMax;
                Bootstrap.Instance.EventMgr.StoveWarning?.Invoke();
            }
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.ChangeStoveCounterState -= OnStoveCounterState;
            Bootstrap.Instance.EventMgr.UpdateCounterProgress -= OnCounterProgressChanged;
        }

        private void OnStoveCounterState(int senderID, StoveCounterState state)
        {
            if (_stoveCounterObj.GetInstanceID() != senderID)
            {
                return;
            }

            if (state is StoveCounterState.Frying or StoveCounterState.Fried)
            {
                _audioSrc.Play();
            }
            else
            {
                _audioSrc.Stop();
            }
        }

        private void OnCounterProgressChanged(int senderID, float progressNormalized)
        {
            if (senderID != _stoveCounter.gameObject.GetInstanceID())
            {
                return;
            }

            _shouldPlayWarningSound = _stoveCounter.IsFried && progressNormalized >= _config.BurnProgressAmount;
        }
    }
}
