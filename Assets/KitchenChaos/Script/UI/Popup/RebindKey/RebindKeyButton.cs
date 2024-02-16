using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    public sealed class RebindKeyButton : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private RebindKeyButtonCfg _config;

        [Header("Internal Ref")]
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _text;

        private void Awake()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnEnable()
        {
            UpdateText();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void OnClicked()
        {
            Bootstrap.Instance.UIManager.Push(ScreenID.RebindKey);

            string actionName = _config.RebindKeySO.GetActionName();
            int index = _config.RebindKeySO.Index;

            Bootstrap.Instance.InputMgr.RebindBinding(actionName, index, () =>
            {
                Bootstrap.Instance.UIManager.Pop();
                UpdateText();
            });
        }

        private void UpdateText()
        {
            _text.SetText(_config.RebindKeySO.GetDisplayString());
        }
    }
}
