using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    public sealed class ProgressBarUI : MonoBehaviour
    {
        private const int MAX_PROGRESS = 1;
        private const int MIN_PROGRESS = 0;

        [Header("External Ref")]
        [SerializeField] private GameObject _progressCounterObj;

        [Header("Internal Ref")]
        [SerializeField] private Image _progressImg;

        private void Start()
        {
            Bootstrap.Instance.EventMgr.UpdateCounterProgress += OnCounterProgressChanged;

            _progressImg.fillAmount = MIN_PROGRESS;
            HideProgressBar();
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

            if (progressNormalized <= MIN_PROGRESS || progressNormalized >= MAX_PROGRESS)
            {
                HideProgressBar();
            }
            else
            {
                ShowProgressBar();
            }
        }

        private void ShowProgressBar()
        {
            gameObject.SetActive(true);
        }

        private void HideProgressBar()
        {
            gameObject.SetActive(false);
        }
    }
}
