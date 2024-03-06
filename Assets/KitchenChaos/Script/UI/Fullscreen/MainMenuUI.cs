using KitchenChaos.Utils;
using UISystem;
using UnityEngine;
using UnityEngine.UI;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class MainMenuUI : BaseScreen
    {
        [Header("Config")]
        [SerializeField] private MainMenuUICfg? _config;

        [Header("Internal Ref")]
        [SerializeField] private Button? _playBtn;
        [SerializeField] private Button? _quitBtn;

        private GameStateManager? _gameStateMgr;

        private GameObject? _decorationObj;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void Awake()
        {
            RegisterServices();
        }

        private void OnDestroy()
        {
            DeregisterServices();
        }

        public override void OnPush(object[]? datas = null)
        {
            _playBtn!.onClick.AddListener(OnPlayButtonClicked);
            _quitBtn!.onClick.AddListener(OnQuitButtonClicked);

            _decorationObj = Instantiate(_config!.DecorationPrefab);

            _playBtn.Select();

            Time.timeScale = 1;
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
            _playBtn!.onClick.RemoveAllListeners();
            _quitBtn!.onClick.RemoveAllListeners();

            Destroy(gameObject);
            Destroy(_decorationObj);
        }

        private void OnPlayButtonClicked()
        {
            _gameStateMgr!.ChangeState(GameState.WaitingToStart);
        }

        private void OnQuitButtonClicked()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void Show()
        {
            gameObject.SetActive(true);
            _decorationObj!.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            _decorationObj!.SetActive(false);
        }

        private void CheckNullEditorReferences()
        {
            if (_config == null || _playBtn == null || _quitBtn == null)
            {
                CustomLog.LogError(this, "missing references in editor!!!");
            }
        }

        private void RegisterServices()
        {
            _gameStateMgr = ServiceLocator.Instance.Get<GameStateManager>();
        }

        private void DeregisterServices()
        {
            _gameStateMgr = null;
        }
    }
}
