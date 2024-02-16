using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    public sealed class ProgressBarFlashingUI : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private ProgressBarFlashingUICfg _config;

        [Header("Internal Ref")]
        [SerializeField] private StoveCounter _stoveCounter;
        [SerializeField] private Image _progressBarImg;

        private Sequence _flashingSequence;

        private void Start()
        {
            Bootstrap.Instance.EventMgr.UpdateCounterProgress += OnCounterProgressChanged;

            PrepareFlashingSequence();
            ResetProgressBarColor();
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.UpdateCounterProgress -= OnCounterProgressChanged;
        }

        private void OnCounterProgressChanged(int senderID, float progressNormalized)
        {
            if (senderID != _stoveCounter.gameObject.GetInstanceID())
            {
                return;
            }

            if (_stoveCounter.IsFried && progressNormalized >= _config.BurnProgressAmount)
            {
                if (!_flashingSequence.IsPlaying())
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
            _flashingSequence.Restart();
        }

        private void ResetProgressBarColor()
        {
            _flashingSequence.Pause();
            _progressBarImg.color = _config.IdleColor;
        }

        private void PrepareFlashingSequence()
        {
            _flashingSequence = DOTween.Sequence()
                .Append(_progressBarImg.DOColor(_config.WarningColor, 0.1f))
                .Append(_progressBarImg.DOColor(_config.IdleColor, 0.1f))
                .SetLoops(-1, LoopType.Restart);
        }
    }
}
