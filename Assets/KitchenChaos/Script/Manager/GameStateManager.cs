using KitchenChaos.Utils;
using UISystem;
using UnityEngine;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class GameStateManager : MonoBehaviour
    {
        public float PlayingTimerNormalized => _playingTimer / _config!.PlayingTimerMax;
        public float CountDownTimer => _countdownTimer;

        [Header("Config")]
        [SerializeField] private GameStateManagerCfg? _config;

        private EventManager? _eventMgr;
        private UIManager? _uiMgr;

        private GameState _curState;
        private GameObject? _playerObj;
        private GameObject? _levelObj;
        private float _countdownTimer;
        private float _playingTimer;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        public void Init()
        {
            RegisterServices();
            SubscribeEvents();

            _countdownTimer = _config!.CountdownTimerMax;
        }

        private void Update()
        {
            switch (_curState)
            {
                case GameState.CountDownToStart:
                    _countdownTimer -= Time.deltaTime;
                    if (_countdownTimer < _config!.CountdownTimerMin)
                    {
                        ChangeState(GameState.GamePlaying);
                    }

                    break;
                case GameState.GamePlaying:
                    _playingTimer += Time.deltaTime;
                    if (_playingTimer >= _config!.PlayingTimerMax)
                    {
                        ChangeState(GameState.GameOver);
                    }

                    break;
            }
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
            DeregisterServices();
        }

        public void ChangeState(GameState state)
        {
            switch (state)
            {
                case GameState.MainMenu:
                    _countdownTimer = _config!.CountdownTimerMax;
                    _playingTimer = _config.PlayingTimerMin;

                    if (_playerObj != null)
                    {
                        Destroy(_playerObj);
                        _playerObj = null;
                    }

                    if (_levelObj != null)
                    {
                        Destroy(_levelObj);
                        _levelObj = null;
                    }

                    _uiMgr!.Push(ScreenID.MainMenu);

                    break;
                case GameState.WaitingToStart:
                    _uiMgr!.Push(ScreenID.ActionPhase);

                    _playerObj = Instantiate(_config!.PlayerPrefab);
                    _levelObj = Instantiate(_config.LevelOnePrefab);

                    _uiMgr.Push(ScreenID.Tutorial);
                    break;
                case GameState.GamePlaying:
                    _countdownTimer = _config!.CountdownTimerMax;
                    break;
                case GameState.GameOver:
                    _playingTimer = _config!.PlayingTimerMin;
                    _uiMgr!.Push(ScreenID.GameOver);
                    break;
            }

            _curState = state;
            _eventMgr!.ChangeGameState?.Invoke(_curState);
        }

        private void OnInteract()
        {
            if (_curState is GameState.WaitingToStart)
            {
                ChangeState(GameState.CountDownToStart);
            }
        }

        private void OnTogglePaused()
        {
            if (_curState is GameState.MainMenu or GameState.GameOver)
            {
                return;
            }

            if (Time.timeScale == 1)
            {
                _uiMgr!.Push(ScreenID.GamePause);
            }
            else
            {
                _uiMgr!.Pop();
            }
        }

        private void CheckNullEditorReferences()
        {
            if (_config == null)
            {
                CustomLog.LogError(this, "missing internal references in editor!");
            }
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
            _eventMgr!.TogglePause += OnTogglePaused;
            _eventMgr!.Interact += OnInteract;
        }

        private void UnsubscribeEvents()
        {
            _eventMgr!.TogglePause -= OnTogglePaused;
            _eventMgr!.Interact -= OnInteract;
        }
    }
}
