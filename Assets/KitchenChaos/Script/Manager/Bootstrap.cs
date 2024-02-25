using UnityServiceLocator;
using UISystem;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class Bootstrap : MonoBehaviour
    {
        public static Bootstrap Instance => _instance;
        private static Bootstrap _instance;

        [Header("Internal Ref")]
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private SFXManager _sfxMgr;
        [SerializeField] private MusicManager _musicMgr;
        [SerializeField] private DeliveryManager _deliveryMgr;
        [SerializeField] private GameStateManager _gameStateMgr;
        [SerializeField] private InputManager _inputMgr;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }

            RegisterManagers();
            _sceneLoader.LoadAsync(SceneState.Gameplay, () => _gameStateMgr.ChangeState(GameState.MainMenu));
        }

        private void RegisterManagers()
        {
            ServiceLocator.Instance.Register(new EventManager());
            ServiceLocator.Instance.Register(_uiManager, () => _uiManager.Init());
            ServiceLocator.Instance.Register(_gameStateMgr, () => _gameStateMgr.Init());
            ServiceLocator.Instance.Register(_inputMgr, () => _inputMgr.Init());
            ServiceLocator.Instance.Register(_deliveryMgr, () => _deliveryMgr.Init());
            ServiceLocator.Instance.Register(_sfxMgr, () => _sfxMgr.Init());
            ServiceLocator.Instance.Register(_musicMgr, () => _musicMgr.Init());
        }
    }
}
