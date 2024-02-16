using UISystem;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class Bootstrap : MonoBehaviour
    {
        public static Bootstrap Instance => _instance;
        private static Bootstrap _instance;

        public EventManager EventMgr => _eventMgr;
        public GameStateManager GameStateMgr => _gameStateMgr;
        public InputManager InputMgr => _inputMgr;
        public DeliveryManager DeliveryMgr => _deliveryMgr;
        public SFXManager SFXMgr => _sfxMgr;
        public MusicManager MusicMgr => _musicMgr;
        public UIManager UIManager => _uiManager;

        [Header("Internal Ref")]
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private SFXManager _sfxMgr;
        [SerializeField] private MusicManager _musicMgr;
        [SerializeField] private DeliveryManager _deliveryMgr;
        [SerializeField] private GameStateManager _gameStateMgr;

        private EventManager _eventMgr;
        private InputManager _inputMgr;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }

            InitManagers();
            _sceneLoader.LoadAsync(SceneState.Gameplay, () => _gameStateMgr.ChangeState(GameState.MainMenu));
        }

        private void OnDestroy()
        {
            _deliveryMgr.OnDestroy();
            _inputMgr.OnDestroy();
            _gameStateMgr.OnDestroy();
        }

        private void InitManagers()
        {
            _eventMgr = new EventManager();
            _uiManager.Init();
            _gameStateMgr.Init();
            _inputMgr = new InputManager();
            _deliveryMgr.Init();
            _sfxMgr.Init();
        }
    }
}
