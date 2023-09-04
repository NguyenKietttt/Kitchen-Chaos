using UnityEngine;
using UnityEngine.UI;

public sealed class ProgressBarUI : MonoBehaviour
{
    [Header("Internal Ref")]
    [SerializeField] private GameObject _progressCounterObj;
    [SerializeField] private Image _progressImg;

    private void Start()
    {
        Bootstrap.Instance.EventMgr.OnProgressChanged += OnProgressChanged;

        _progressImg.fillAmount = 0;
        ToggleProgressBar(false);
    }

    private void OnDestroy()
    {
        Bootstrap.Instance.EventMgr.OnProgressChanged -= OnProgressChanged;
    }

    private void OnProgressChanged(float progressNormalized, int counterInstanceID)
    {
        if (counterInstanceID != _progressCounterObj.GetInstanceID())
        {
            return;
        }

        _progressImg.fillAmount = progressNormalized;

        if (progressNormalized <= 0 || progressNormalized >= 1)
        {
            ToggleProgressBar(false);
        }
        else
        {
            ToggleProgressBar(true);
        }
    }

    private void ToggleProgressBar(bool isEnabled)
    {
        gameObject.SetActive(isEnabled);
    }
}
