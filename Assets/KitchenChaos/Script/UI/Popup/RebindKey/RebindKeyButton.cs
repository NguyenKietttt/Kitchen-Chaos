using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    public sealed class RebindKeyButton : MonoBehaviour
    {
        [Header("Asset Ref")]
        [SerializeField] private RebindKeySO _rebindKeySO;

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

            string actionName = _rebindKeySO.GetActionName();
            int index = _rebindKeySO.Index;

            Bootstrap.Instance.InputMgr.RebindBinding(actionName, index, () =>
            {
                Bootstrap.Instance.UIManager.Pop();
                UpdateText();
            });
        }

        private void UpdateText()
        {
            _text.SetText(_rebindKeySO.GetDisplayString());
        }
    }
}
