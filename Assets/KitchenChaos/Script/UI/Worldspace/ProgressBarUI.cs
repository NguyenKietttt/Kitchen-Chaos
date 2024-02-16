using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    public sealed class ProgressBarUI : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private ProgressBarUICfg _config;

        [Header("External Ref")]
        [SerializeField] private GameObject _progressCounterObj;

        [Header("Internal Ref")]
        [SerializeField] private Image _progressImg;

        private void Start()
        {
            Bootstrap.Instance.EventMgr.UpdateCounterProgress += OnCounterProgressChanged;

            _progressImg.fillAmount = _config.MinProgress;
            Hide();
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.UpdateCounterProgress -= OnCounterProgressChanged;
        }

        private void OnCounterProgressChanged(int senderID, float progressNormalized)
        {
            if (senderID != _progressCounterObj.GetInstanceID())
            {
                return;
            }

            _progressImg.fillAmount = progressNormalized;

            if (progressNormalized <= _config.MinProgress || progressNormalized >= _config.MaxProgress)
            {
                Hide();
            }
            else
            {
                Show();
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
}
