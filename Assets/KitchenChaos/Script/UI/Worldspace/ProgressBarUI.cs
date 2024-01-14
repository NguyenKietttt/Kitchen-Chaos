using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    public sealed class ProgressBarUI : MonoBehaviour
    {
        [Header("External Ref")]
        [SerializeField] private GameObject _progressCounterObj;

        [Header("Internal Ref")]
        [SerializeField] private Image _progressImg;

        private void Start()
        {
            Bootstrap.Instance.EventMgr.UpdateCounterProgress += OnCounterProgressChanged;

            _progressImg.fillAmount = 0;
            ToggleProgressBar(false);
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.UpdateCounterProgress -= OnCounterProgressChanged;
        }

        private void OnCounterProgressChanged(float progressNormalized, int counterInstanceID)
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
}
