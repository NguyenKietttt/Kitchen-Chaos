using UnityEngine;

public sealed class StoveWarningUI : MonoBehaviour
{
    private const float BURN_PROGRESS_AMOUNT = 0.5f;

    [Header("Internal Ref")]
    [SerializeField] private StoveCounter _stoveCounter;

    private void Start()
    {
        Bootstrap.Instance.EventMgr.UpdateCounterProgress += OnCounterProgressChanged;
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
}
