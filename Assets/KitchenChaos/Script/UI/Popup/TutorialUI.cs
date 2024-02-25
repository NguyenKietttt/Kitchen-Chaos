using TMPro;
using UISystem;
using UnityEngine;
using UnityServiceLocator;

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

        private EventManager _eventMgr;
        private UIManager _uiMgr;

        public override void OnPush(object[] datas = null)
        {
            RegisterServices();
            SubscribeEvents();

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
            UnsubscribeEvents();
            DeregisterServices();

            Destroy(gameObject);
        }

        private void OnGameStateChanged(GameState state)
        {
            if (state is GameState.CountDownToStart)
            {
                _uiMgr.Pop();
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

        private void RegisterServices()
        {
            _eventMgr = ServiceLocator.Instance.Get<EventManager>();
            _uiMgr = ServiceLocator.Instance.Get<UIManager>();
        }

        private void DeregisterServices()
        {
            _eventMgr = null;
            _uiMgr = null;
        }

        private void SubscribeEvents()
        {
            _eventMgr.RebindKey += UpdateVisual;
            _eventMgr.ChangeGameState += OnGameStateChanged;
        }

        private void UnsubscribeEvents()
        {
            _eventMgr.RebindKey -= UpdateVisual;
            _eventMgr.ChangeGameState -= OnGameStateChanged;
        }
    }
}
