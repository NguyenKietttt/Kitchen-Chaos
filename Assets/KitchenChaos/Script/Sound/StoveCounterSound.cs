using UnityEngine;

namespace KitchenChaos
{
    public sealed class StoveCounterSound : MonoBehaviour
    {
        private const float BURN_PROGRESS_AMOUNT = 0.5f;
        private const float WARNING_SOUND_TIMER_MAX = 0.2f;
        private const float WARNING_SOUND_TIMER_MIN = 0.0f;

        [Header("External Ref")]
        [SerializeField] private GameObject _stoveCounterObj;

        [Header("Internal Ref")]
        [SerializeField] private StoveCounter _stoveCounter;
        [SerializeField] private AudioSource _audioSrc;

        private float _warningSoundTimer;
        private bool _shoudPlayWarningSound;

        private void Start()
        {
            Bootstrap.Instance.EventMgr.ChangeStoveCounterState += OnStoveCounterState;
            Bootstrap.Instance.EventMgr.UpdateCounterProgress += OnCounterProgressChanged;
        }

        private void Update()
        {
            if (!_shoudPlayWarningSound)
            {
                _warningSoundTimer = WARNING_SOUND_TIMER_MAX;
                return;
            }

            _warningSoundTimer -= Time.deltaTime;
            if (_warningSoundTimer <= WARNING_SOUND_TIMER_MIN)
            {
                _warningSoundTimer = WARNING_SOUND_TIMER_MAX;
                Bootstrap.Instance.EventMgr.StoveWarning?.Invoke();
            }
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.ChangeStoveCounterState -= OnStoveCounterState;
            Bootstrap.Instance.EventMgr.UpdateCounterProgress -= OnCounterProgressChanged;
        }

        private void OnStoveCounterState(int senderID, StoveCounter.State state)
        {
            if (_stoveCounterObj.GetInstanceID() != senderID)
            {
                return;
            }

            if (state is StoveCounter.State.Frying or StoveCounter.State.Fried)
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

            _shoudPlayWarningSound = _stoveCounter.IsFried && progressNormalized >= BURN_PROGRESS_AMOUNT;
        }
    }
}
