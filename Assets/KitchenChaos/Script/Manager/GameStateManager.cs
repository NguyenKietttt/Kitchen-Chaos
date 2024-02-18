using UnityEngine;

namespace KitchenChaos
{
    public sealed class GameStateManager : MonoBehaviour
    {
        public float PlayingTimerNormalized => _playingTimer / _config.PlayingTimerMax;
        public float CountDownTimer => _countdownTimer;

        [Header("Config")]
        [SerializeField] private GameStateManagerCfg _config;

        private GameState _curState;
        private GameObject _playerObj;
        private GameObject _levelObj;
        private float _countdownTimer;
        private float _playingTimer;

        public void Init()
        {
            Bootstrap.Instance.EventMgr.TogglePause += OnTogglePaused;
            Bootstrap.Instance.EventMgr.Interact += OnInteract;

            _countdownTimer = _config.CoundownTimerMax;
        }

        private void Update()
        {
            switch (_curState)
            {
                case GameState.CountDownToStart:
                    _countdownTimer -= Time.deltaTime;
                    if (_countdownTimer < _config.CoundownTimerMin)
                    {
                        ChangeState(GameState.GamePlaying);
                    }

                    break;
                case GameState.GamePlaying:
                    _playingTimer += Time.deltaTime;
                    if (_playingTimer >= _config.PlayingTimerMax)
                    {
                        ChangeState(GameState.GameOver);
                    }

                    break;
            }
        }

        public void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.TogglePause -= OnTogglePaused;
            Bootstrap.Instance.EventMgr.Interact -= OnInteract;
        }

        public void ChangeState(GameState state)
        {
            switch (state)
            {
                case GameState.MainMenu:
                    _countdownTimer = _config.CoundownTimerMax;
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

                    Bootstrap.Instance.UIManager.Push(UISystem.ScreenID.MainMenu);

                    break;
                case GameState.WaitingToStart:
                    Bootstrap.Instance.UIManager.Push(UISystem.ScreenID.ActionPhase);

                    _playerObj = Instantiate(_config.PlayerPrefab);
                    _levelObj = Instantiate(_config.LevelOnePrefab);

                    Bootstrap.Instance.UIManager.Push(UISystem.ScreenID.Tutorial);
                    break;
                case GameState.GamePlaying:
                    _countdownTimer = _config.CoundownTimerMax;
                    break;
                case GameState.GameOver:
                    _playingTimer = _config.PlayingTimerMin;
                    Bootstrap.Instance.UIManager.Push(UISystem.ScreenID.GameOver);
                    break;
            }

            _curState = state;
            Bootstrap.Instance.EventMgr.ChangeGameState?.Invoke(_curState);
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
                Bootstrap.Instance.UIManager.Push(UISystem.ScreenID.GamePause);
            }
            else
            {
                Bootstrap.Instance.UIManager.Pop();
            }
        }
    }
}
