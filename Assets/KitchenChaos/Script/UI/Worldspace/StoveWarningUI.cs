using DG.Tweening;
using KitchenChaos.Utils;
using UnityEngine;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class StoveWarningUI : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private StoveWarningUICfg? _config;

        [Header("Internal Ref")]
        [SerializeField] private StoveCounter? _stoveCounter;
        [SerializeField] private CanvasGroup? _canvasGroup;

        private EventManager? _eventMgr;

        private Sequence? _warningSequence;

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

            SetupWarningSequence();
            Hide();
        }

        private void OnDestroy()
        {
            DOTween.Clear(true);

            UnsubscribeEvents();
            DeregisterServices();
        }

        private void OnCounterProgressChanged(int senderID, float progressNormalized)
        {
            if (senderID != _stoveCounter!.gameObject.GetInstanceID())
            {
                return;
            }

            if (_stoveCounter!.IsFried && progressNormalized >= _config!.BurnProgressAmount)
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
                .AppendCallback(() => _canvasGroup!.alpha = 0)
                .Append(_canvasGroup!.DOFade(1.0f, 0.1f))
                .Append(_canvasGroup!.DOFade(0.0f, 0.2f));

            _warningSequence!.SetLoops(-1, LoopType.Restart);
        }

        private void CheckNullEditorReferences()
        {
            if (_config == null || _stoveCounter == null || _canvasGroup == null)
            {
                CustomLog.LogError(this, "missing internal references in editor!");
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
