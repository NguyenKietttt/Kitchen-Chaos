using KitchenChaos.Utils;
using UnityEngine;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class StoveCounterSound : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private StoveCounterSFXCfg? _config;

        [Header("External Ref")]
        [SerializeField] private GameObject? _stoveCounterObj;

        [Header("Internal Ref")]
        [SerializeField] private StoveCounter? _stoveCounter;
        [SerializeField] private AudioSource? _audioSrc;

        private EventManager? _eventMgr;

        private float _warningSoundTimer;
        private bool _shouldPlayWarningSound;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void Awake()
        {
            RegisterServices();
        }

        private void Start()
        {
            SubscribeEvents();
        }

        private void Update()
        {
            if (!_shouldPlayWarningSound)
            {
                _warningSoundTimer = _config!.WarningTimerMax;
                return;
            }

            _warningSoundTimer -= Time.deltaTime;
            if (_warningSoundTimer <= _config!.WarningTimerMin)
            {
                _warningSoundTimer = _config!.WarningTimerMax;
                _eventMgr!.StoveWarning?.Invoke();
            }
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
            DeregisterServices();
        }

        private void OnStoveCounterState(int senderID, StoveCounterState state)
        {
            if (_stoveCounterObj!.GetInstanceID() != senderID)
            {
                return;
            }

            if (state is StoveCounterState.Frying or StoveCounterState.Fried)
            {
                _audioSrc!.Play();
            }
            else
            {
                _audioSrc!.Stop();
            }
        }

        private void OnCounterProgressChanged(int senderID, float progressNormalized)
        {
            if (senderID != _stoveCounter!.gameObject.GetInstanceID())
            {
                return;
            }

            _shouldPlayWarningSound = _stoveCounter.IsFried && progressNormalized >= _config!.BurnProgressAmount;
        }

        private void CheckNullEditorReferences()
        {
            if (_config == null || _stoveCounterObj == null || _stoveCounter == null || _audioSrc == null)
            {
                CustomLog.LogError(this, "missing references in editor!!!");
            }
        }

        private void RegisterServices()
        {
            _eventMgr = ServiceLocator.Instance.Get<EventManager>();
        }

        private void DeregisterServices()
        {
            _eventMgr = null;
        }

        private void SubscribeEvents()
        {
            _eventMgr!.ChangeStoveCounterState += OnStoveCounterState;
            _eventMgr!.UpdateCounterProgress += OnCounterProgressChanged;
        }

        private void UnsubscribeEvents()
        {
            _eventMgr!.ChangeStoveCounterState -= OnStoveCounterState;
            _eventMgr!.UpdateCounterProgress -= OnCounterProgressChanged;
        }
    }
}
