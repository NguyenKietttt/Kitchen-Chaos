using DG.Tweening;
using UnityEngine;

public sealed class StoveWarningUI : MonoBehaviour
{
    private const float BURN_PROGRESS_AMOUNT = 0.5f;

    [Header("Internal Ref")]
    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private CanvasGroup _canvasGroup;

    private Sequence _warningSequence;

    private void Start()
    {
        Bootstrap.Instance.EventMgr.UpdateCounterProgress += OnCounterProgressChanged;

        SetupWarningSequence();
        Hide();
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
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void SetupWarningSequence()
    {
        _warningSequence = DOTween.Sequence()
            .AppendCallback(() => _canvasGroup.alpha = 0)
            .Append(_canvasGroup.DOFade(1.0f, 0.1f))
            .Append(_canvasGroup.DOFade(0.0f, 0.2f));

        _warningSequence.SetLoops(-1, LoopType.Restart);
    }
}
