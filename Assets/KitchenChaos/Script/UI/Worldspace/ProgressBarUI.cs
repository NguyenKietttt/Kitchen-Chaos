using KitchenChaos.Utils;
using UnityEngine;
using UnityEngine.UI;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class ProgressBarUI : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private ProgressBarUICfg? _config;

        [Header("External Ref")]
        [SerializeField] private GameObject? _progressCounterObj;

        [Header("Internal Ref")]
        [SerializeField] private Image? _progressImg;

        private EventManager? _eventMgr;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void Awake()
        {
            RegisterServices();
        }

        private void Start()
        {
            SubscribeEvents();

            _progressImg!.fillAmount = _config!.MinProgress;
            Hide();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
            DeregisterServices();
        }

        private void OnCounterProgressChanged(int senderID, float progressNormalized)
        {
            if (senderID != _progressCounterObj!.GetInstanceID())
            {
                return;
            }

            _progressImg!.fillAmount = progressNormalized;

            if (progressNormalized <= _config!.MinProgress || progressNormalized >= _config!.MaxProgress)
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

        private void CheckNullEditorReferences()
        {
            if (_config == null || _progressCounterObj == null || _progressImg == null)
            {
                CustomLog.LogError(this, "missing references in editor!!!");
            }
        }

        private void RegisterServices()
        {
            _eventMgr = ServiceLocator.Instance.Get<EventManager>();
        }

        private void DeregisterServices()
        {
            _eventMgr = null;
        }

        private void SubscribeEvents()
        {
            _eventMgr!.UpdateCounterProgress += OnCounterProgressChanged;
        }

        private void UnsubscribeEvents()
        {
            _eventMgr!.UpdateCounterProgress -= OnCounterProgressChanged;
        }
    }
}
