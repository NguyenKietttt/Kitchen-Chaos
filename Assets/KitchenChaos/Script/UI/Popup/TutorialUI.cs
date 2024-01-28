using TMPro;
using UISystem;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class TutorialUI : BaseScreen
    {
        [Header("Asset Ref")]
        [SerializeField] private RebindKeySO _rebindKeyMoveUpSO;
        [SerializeField] private RebindKeySO _rebindKeyMoveDownSO;
        [SerializeField] private RebindKeySO _rebindKeyMoveLeftSO;
        [SerializeField] private RebindKeySO _rebindKeyMoveRightSO;
        [SerializeField] private RebindKeySO _rebindKeyInteractSO;
        [SerializeField] private RebindKeySO _rebindKeyCutSO;
        [SerializeField] private RebindKeySO _rebindKeyInteractGamepadSO;
        [SerializeField] private RebindKeySO _rebindKeyCutGamepadSO;

        [Header("Internal Ref")]
        [SerializeField] private TextMeshProUGUI _keyboardMoveUpTxt;
        [SerializeField] private TextMeshProUGUI _keyboardMoveDownTxt;
        [SerializeField] private TextMeshProUGUI _keyboardMoveLeftTxt;
        [SerializeField] private TextMeshProUGUI _keyboardMoveRightTxt;
        [SerializeField] private TextMeshProUGUI _keyboardInteractTxt;
        [SerializeField] private TextMeshProUGUI _keyboardCutTxt;
        [SerializeField] private TextMeshProUGUI _gamepadInteractTxt;
        [SerializeField] private TextMeshProUGUI _gamepadCutTxt;

        public override void OnPush(object[] datas = null)
        {
            Bootstrap.Instance.EventMgr.RebindingKey += UpdateVisual;
            Bootstrap.Instance.EventMgr.ChangeGameState += OnGameStateChanged;

            UpdateVisual();
        }

        public override void OnFocus()
        {
            Show();
        }

        public override void OnFocusLost()
        {
            Hide();
        }

        public override void OnPop()
        {
            Bootstrap.Instance.EventMgr.RebindingKey -= UpdateVisual;
            Bootstrap.Instance.EventMgr.ChangeGameState -= OnGameStateChanged;

            Destroy(gameObject);
        }

        private void OnGameStateChanged(GameState state)
        {
            if (state is GameState.CountDownToStart)
            {
                Bootstrap.Instance.UIManager.Pop();
            }
        }

        private void UpdateVisual()
        {
            _keyboardMoveUpTxt.SetText(_rebindKeyMoveUpSO.GetDisplayString());
            _keyboardMoveDownTxt.SetText(_rebindKeyMoveDownSO.GetDisplayString());
            _keyboardMoveLeftTxt.SetText(_rebindKeyMoveLeftSO.GetDisplayString());
            _keyboardMoveRightTxt.SetText(_rebindKeyMoveRightSO.GetDisplayString());
            _keyboardInteractTxt.SetText(_rebindKeyInteractSO.GetDisplayString());
            _keyboardCutTxt.SetText(_rebindKeyCutSO.GetDisplayString());

            _gamepadInteractTxt.SetText(_rebindKeyInteractGamepadSO.GetDisplayString());
            _gamepadCutTxt.SetText(_rebindKeyCutGamepadSO.GetDisplayString());
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
