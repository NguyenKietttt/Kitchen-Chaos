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
        public GameObject PlayerPrefab => _config.PlayerPrefab;
        public GameObject LevelOnePrefab => _config.LevelOnePrefab;

        [Header("Asset Ref")]
        [SerializeField] private BootstrapCfg _config;

        [Header("Internal Ref")]
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private SFXManager _sfxMgr;
        [SerializeField] private MusicManager _musicMgr;
        [SerializeField] private DeliveryManager _deliveryMgr;

        private EventManager _eventMgr;
        private GameStateManager _gameStateMgr;
        private InputManager _inputMgr;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }

            InitManagers();
            _sceneLoader.LoadAsync(SceneState.Gameplay, () => GameStateMgr.Init());
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            GameStateMgr.OnUpdate(deltaTime);
            DeliveryMgr.OnUpdate(deltaTime);
        }

        private void OnDestroy()
        {
            DeliveryMgr.OnDestroy();
            InputMgr.OnDestroy();
            GameStateMgr.OnDestroy();
        }

        private void InitManagers()
        {
            _eventMgr = new EventManager();
            _uiManager.Init();
            _gameStateMgr = new GameStateManager();
            _inputMgr = new InputManager();
            _deliveryMgr.Init();
            _sfxMgr.Init();
        }
    }
}
