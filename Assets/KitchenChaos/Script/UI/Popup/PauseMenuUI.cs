using KitchenChaos.Utils;
using UISystem;
using UnityEngine;
using UnityEngine.UI;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class PauseMenuUI : BaseScreen
    {
        [Header("Internal Ref")]
        [SerializeField] private Button? _resumeBtn;
        [SerializeField] private Button? _optionsBtn;
        [SerializeField] private Button? _mainMenuBtn;

        private EventManager? _eventMgr;
        private GameStateManager? _gameStateMgr;
        private UIManager? _uiMgr;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        public override void OnPush(object[]? datas = null)
        {
            RegisterServices();
            SubscribeEvents();

            Time.timeScale = 0;
        }

        public override void OnFocus()
        {
            Show();
            Time.timeScale = 0;
        }

        public override void OnFocusLost()
        {
            Hide();
            Time.timeScale = 1;
        }

        public override void OnPop()
        {
            UnsubscribeEvents();
            DeregisterServices();

            Destroy(gameObject);
            Time.timeScale = 1;
        }

        private void OnResumeButtonClicked()
        {
            _eventMgr!.TogglePause?.Invoke();
        }

        private void OnOptionsButtonClicked()
        {
            _uiMgr!.Push(ScreenID.Option);
        }

        private void OnMainMenuButtonClicked()
        {
            _gameStateMgr!.ChangeState(GameState.MainMenu);
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
            if (_resumeBtn == null || _optionsBtn == null || _mainMenuBtn == null)
            {
                CustomLog.LogError(this, "missing references in editor!!!");
            }
        }

        private void RegisterServices()
        {
            _eventMgr = ServiceLocator.Instance.Get<EventManager>();
            _gameStateMgr = ServiceLocator.Instance.Get<GameStateManager>();
            _uiMgr = ServiceLocator.Instance.Get<UIManager>();
        }

        private void DeregisterServices()
        {
            _eventMgr = null;
            _gameStateMgr = null;
            _uiMgr = null;
        }

        private void SubscribeEvents()
        {
            _resumeBtn!.onClick.AddListener(OnResumeButtonClicked);
            _optionsBtn!.onClick.AddListener(OnOptionsButtonClicked);
            _mainMenuBtn!.onClick.AddListener(OnMainMenuButtonClicked);
        }

        private void UnsubscribeEvents()
        {
            _resumeBtn!.onClick.RemoveAllListeners();
            _optionsBtn!.onClick.RemoveAllListeners();
            _mainMenuBtn!.onClick.RemoveAllListeners();
        }
    }
}
