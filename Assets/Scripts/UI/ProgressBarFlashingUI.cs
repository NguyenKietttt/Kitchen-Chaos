using UnityEngine;

public sealed class ProgressBarFlashingUI : MonoBehaviour
{
    private const float BURN_PROGRESS_AMOUNT = 0.5f;

    [Header("Internal Ref")]
    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private Animator _animator;

    private readonly int _isFlashingHashKey = Animator.StringToHash("IsFlashing");

    private void Start()
    {
        Bootstrap.Instance.EventMgr.UpdateCounterProgress += OnCounterProgressChanged;
        _animator.SetBool(_isFlashingHashKey, false);
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

        _animator.SetBool(_isFlashingHashKey, _stoveCounter.IsFried && progressNormalized >= BURN_PROGRESS_AMOUNT);
    }
}
