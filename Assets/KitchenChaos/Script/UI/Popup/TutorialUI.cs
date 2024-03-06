using KitchenChaos.Utils;
using TMPro;
using UISystem;
using UnityEngine;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class TutorialUI : BaseScreen
    {
        [Header("Asset Ref")]
        [SerializeField] private RebindKeySO? _rebindKeyMoveUpSO;
        [SerializeField] private RebindKeySO? _rebindKeyMoveDownSO;
        [SerializeField] private RebindKeySO? _rebindKeyMoveLeftSO;
        [SerializeField] private RebindKeySO? _rebindKeyMoveRightSO;
        [SerializeField] private RebindKeySO? _rebindKeyInteractSO;
        [SerializeField] private RebindKeySO? _rebindKeyCutSO;
        [SerializeField] private RebindKeySO? _rebindKeyInteractGamepadSO;
        [SerializeField] private RebindKeySO? _rebindKeyCutGamepadSO;

        [Header("Internal Ref")]
        [SerializeField] private TextMeshProUGUI? _keyboardMoveUpTxt;
        [SerializeField] private TextMeshProUGUI? _keyboardMoveDownTxt;
        [SerializeField] private TextMeshProUGUI? _keyboardMoveLeftTxt;
        [SerializeField] private TextMeshProUGUI? _keyboardMoveRightTxt;
        [SerializeField] private TextMeshProUGUI? _keyboardInteractTxt;
        [SerializeField] private TextMeshProUGUI? _keyboardCutTxt;
        [SerializeField] private TextMeshProUGUI? _gamepadInteractTxt;
        [SerializeField] private TextMeshProUGUI? _gamepadCutTxt;

        private EventManager? _eventMgr;
        private UIManager? _uiMgr;
        private InputManager? _inputMgr;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        public override void OnPush(object[]? datas = null)
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
                _uiMgr!.Pop();
            }
        }

        private void UpdateVisual()
        {
            _keyboardMoveUpTxt!.SetText(GetKeyDisplayString(_rebindKeyMoveUpSO!));
            _keyboardMoveDownTxt!.SetText(GetKeyDisplayString(_rebindKeyMoveDownSO!));
            _keyboardMoveLeftTxt!.SetText(GetKeyDisplayString(_rebindKeyMoveLeftSO!));
            _keyboardMoveRightTxt!.SetText(GetKeyDisplayString(_rebindKeyMoveRightSO!));
            _keyboardInteractTxt!.SetText(GetKeyDisplayString(_rebindKeyInteractSO!));
            _keyboardCutTxt!.SetText(GetKeyDisplayString(_rebindKeyCutSO!));

            _gamepadInteractTxt!.SetText(GetKeyDisplayString(_rebindKeyInteractGamepadSO!));
            _gamepadCutTxt!.SetText(GetKeyDisplayString(_rebindKeyCutGamepadSO!));
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private string GetKeyDisplayString(RebindKeySO rebindKeySO)
        {
            string actionName = rebindKeySO.ActionName;
            int index = rebindKeySO.Index;
            string displayString = _inputMgr!.GetKeyDisplayString(actionName, index);

            return displayString;
        }

        private void CheckNullEditorReferences()
        {
            if (_rebindKeyMoveUpSO == null || _rebindKeyMoveDownSO == null || _rebindKeyMoveLeftSO == null || _rebindKeyMoveRightSO == null
                || _rebindKeyInteractSO == null || _rebindKeyCutSO == null
                || _rebindKeyInteractGamepadSO == null || _rebindKeyCutGamepadSO == null
                || _keyboardMoveUpTxt == null || _keyboardMoveDownTxt == null || _keyboardMoveLeftTxt == null || _keyboardMoveRightTxt == null
                || _keyboardInteractTxt == null || _keyboardCutTxt == null
                || _gamepadInteractTxt == null || _gamepadCutTxt == null)
            {
                CustomLog.LogError(this, "missing references in editor!!!");
            }
        }

        private void RegisterServices()
        {
            _eventMgr = ServiceLocator.Instance.Get<EventManager>();
            _uiMgr = ServiceLocator.Instance.Get<UIManager>();
            _inputMgr = ServiceLocator.Instance.Get<InputManager>();
        }

        private void DeregisterServices()
        {
            _eventMgr = null;
            _uiMgr = null;
            _inputMgr = null;
        }

        private void SubscribeEvents()
        {
            _eventMgr!.RebindKey += UpdateVisual;
            _eventMgr!.ChangeGameState += OnGameStateChanged;
        }

        private void UnsubscribeEvents()
        {
            _eventMgr!.RebindKey -= UpdateVisual;
            _eventMgr!.ChangeGameState -= OnGameStateChanged;
        }
    }
}
