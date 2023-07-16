using UnityEngine;
using UnityEngine.UI;

public sealed class ProgressBarUI : MonoBehaviour
{
    [Header("Internal Ref")]
    [SerializeField] private CuttingCounter _cuttingCounter;
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

    private void OnProgressChanged(float progressNormalized)
    {
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