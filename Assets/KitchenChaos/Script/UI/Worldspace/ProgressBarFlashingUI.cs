using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public sealed class ProgressBarFlashingUI : MonoBehaviour
{
    private const float BURN_PROGRESS_AMOUNT = 0.5f;

    [Header("Internal Ref")]
    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private Image _progressBarImg;

    [Header("Property")]
    [SerializeField] private Color _idleColor;
    [SerializeField] private Color _warningColor;

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

    private void OnCounterProgressChanged(float progressNormalized, int counterInstanceID)
    {
        if (counterInstanceID != _stoveCounter.gameObject.GetInstanceID())
        {
            return;
        }

        if (_stoveCounter.IsFried && progressNormalized >= BURN_PROGRESS_AMOUNT)
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
        _progressBarImg.color = _idleColor;
    }

    private void PrepareFlashingSequence()
    {
        _flashingSequence = DOTween.Sequence()
            .Append(_progressBarImg.DOColor(_warningColor, 0.1f))
            .Append(_progressBarImg.DOColor(_idleColor, 0.1f))
            .SetLoops(-1, LoopType.Restart);
    }
}
