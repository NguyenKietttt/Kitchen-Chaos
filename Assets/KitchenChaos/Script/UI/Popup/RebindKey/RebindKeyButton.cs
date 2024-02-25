using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class RebindKeyButton : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private RebindKeyButtonCfg _config;

        [Header("Internal Ref")]
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _text;

        private InputManager _inputMgr;
        private UIManager _uiMgr;

        private void Awake()
        {
            RegisterServices();
        }

        private void OnEnable()
        {
            UpdateText();
        }

        private void Start()
        {
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
            DeregisterServices();
        }

        private void OnClicked()
        {
            _uiMgr.Push(ScreenID.RebindKey);

            string actionName = _config.RebindKeySO.ActionName;
            int index = _config.RebindKeySO.Index;

            _inputMgr.RebindBinding(actionName, index, () =>
            {
                _uiMgr.Pop();
                UpdateText();
            });
        }

        private void UpdateText()
        {
            string actionName = _config.RebindKeySO.ActionName;
            int index = _config.RebindKeySO.Index;
            string keyDisplayString = _inputMgr.GetKeyDisplayString(actionName, index);

            _text.SetText(keyDisplayString);
        }

        private void RegisterServices()
        {
            _inputMgr = ServiceLocator.Instance.Get<InputManager>();
            _uiMgr = ServiceLocator.Instance.Get<UIManager>();
        }

        private void DeregisterServices()
        {
            _inputMgr = null;
            _uiMgr = null;
        }

        private void SubscribeEvents()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void UnsubscribeEvents()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
