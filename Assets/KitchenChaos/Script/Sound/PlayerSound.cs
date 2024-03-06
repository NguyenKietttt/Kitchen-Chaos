using KitchenChaos.Utils;
using UnityEngine;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class PlayerSound : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private PlayerCfg? _config;

        [Header("Internal Ref")]
        [SerializeField] private AudioSource? _audioSrc;

        private EventManager? _eventMgr;
        private SFXManager? _sfxMgr;

        private float _footstepTimer;
        private bool _isPlayerMoving;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void Awake()
        {
            RegisterServices();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Update()
        {
            _footstepTimer += Time.deltaTime;
            if (_footstepTimer >= _config!.FootstepTimerMax)
            {
                _footstepTimer = _config.FootstepTimerMin;
                if (_isPlayerMoving)
                {
                    AudioClip footstepClip = _sfxMgr!.GetRandomFootStepAudioClip();
                    _audioSrc!.PlayOneShot(footstepClip);
                }
            }
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void OnDestroy()
        {
            DeregisterServices();
        }

        private void OnPlayerMove()
        {
            _isPlayerMoving = true;
        }

        private void OnPlayerStop()
        {
            _isPlayerMoving = false;
        }

        private void CheckNullEditorReferences()
        {
            if (_config == null || _audioSrc == null)
            {
                CustomLog.LogError(this, "missing internal references in editor!");
            }
        }

        private void RegisterServices()
        {
            _eventMgr = ServiceLocator.Instance.Get<EventManager>();
            _sfxMgr = ServiceLocator.Instance.Get<SFXManager>();
        }

        private void DeregisterServices()
        {
            _eventMgr = null;
            _sfxMgr = null;
        }

        private void SubscribeEvents()
        {
            _eventMgr!.PlayerMove += OnPlayerMove;
            _eventMgr!.PlayerStop += OnPlayerStop;
        }

        private void UnsubscribeEvents()
        {
            _eventMgr!.PlayerMove -= OnPlayerMove;
            _eventMgr!.PlayerStop -= OnPlayerStop;
        }
    }
}
