using DG.Tweening;
using KitchenChaos.Utils;
using UnityEngine;
using UnityEngine.UI;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class ProgressBarFlashingUI : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private ProgressBarFlashingUICfg? _config;

        [Header("Internal Ref")]
        [SerializeField] private StoveCounter? _stoveCounter;
        [SerializeField] private Image? _progressBarImg;

        private EventManager? _eventMgr;

        private Sequence? _flashingSequence;

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

            PrepareFlashingSequence();
            ResetProgressBarColor();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
            DeregisterServices();
        }

        private void OnCounterProgressChanged(int senderID, float progressNormalized)
        {
            if (senderID != _stoveCounter!.gameObject.GetInstanceID())
            {
                return;
            }

            if (_stoveCounter!.IsFried && progressNormalized >= _config!.BurnProgressAmount)
            {
                if (!_flashingSequence!.IsPlaying())
                {
                    PlayFlashingSequence();
                }
            }
            else
            {
                ResetProgressBarColor();
            }
        }

        private void PlayFlashingSequence()
        {
            _flashingSequence!.Restart();
        }

        private void ResetProgressBarColor()
        {
            _flashingSequence!.Pause();
            _progressBarImg!.color = _config!.IdleColor;
        }

        private void PrepareFlashingSequence()
        {
            _flashingSequence = DOTween.Sequence()
                .Append(_progressBarImg!.DOColor(_config!.WarningColor, 0.1f))
                .Append(_progressBarImg!.DOColor(_config!.IdleColor, 0.1f))
                .SetLoops(-1, LoopType.Restart);
        }

        private void CheckNullEditorReferences()
        {
            if (_config == null || _stoveCounter == null || _progressBarImg == null)
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
            _eventMgr!.UpdateCounterProgress += OnCounterProgressChanged;
        }

        private void UnsubscribeEvents()
        {
            _eventMgr!.UpdateCounterProgress -= OnCounterProgressChanged;
        }
    }
}
