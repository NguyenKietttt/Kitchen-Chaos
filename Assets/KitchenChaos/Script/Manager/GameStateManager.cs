using UnityEngine;

namespace KitchenChaos
{
    public sealed class GameStateManager
    {
        private const float COUNTDOWN_TO_START_TIMER_MAX = 3.0f;
        private const float GAME_PLAYING_TIMER_MAX = 60.0f;

        public float GamePlayingTimerNormalized => _gameplayingTimer / GAME_PLAYING_TIMER_MAX;
        public float CountDownToStartTimer => _countdownToStartTimer;

        private GameState _curState;
        private GameObject _playerObj;
        private GameObject _levelObj;
        private float _countdownToStartTimer = COUNTDOWN_TO_START_TIMER_MAX;
        private float _gameplayingTimer;

        public GameStateManager()
        {
            Bootstrap.Instance.EventMgr.TooglePause += OnTogglePaused;
            Bootstrap.Instance.EventMgr.Interact += OnInteract;
        }

        public void Init()
        {
            ChangeState(GameState.MainMenu);
        }

        public void OnUpdate(in float deltaTime)
        {
            switch (_curState)
            {
                case GameState.CountDownToStart:
                    _countdownToStartTimer -= deltaTime;
                    if (_countdownToStartTimer < 0)
                    {
                        ChangeState(GameState.GamePlaying);
                    }

                    break;
                case GameState.GamePlaying:
                    _gameplayingTimer += Time.deltaTime;
                    if (_gameplayingTimer >= GAME_PLAYING_TIMER_MAX)
                    {
                        ChangeState(GameState.GameOver);
                    }

                    break;
            }
        }

        public void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.TooglePause -= OnTogglePaused;
            Bootstrap.Instance.EventMgr.Interact -= OnInteract;
        }

        public void ChangeState(GameState state)
        {
            switch (state)
            {
                case GameState.MainMenu:
                    _countdownToStartTimer = COUNTDOWN_TO_START_TIMER_MAX;
                    _gameplayingTimer = 0;

                    if (_playerObj != null)
                    {
                        Object.Destroy(_playerObj);
                        _playerObj = null;
                    }

                    if (_levelObj != null)
                    {
                        Object.Destroy(_levelObj);
                        _levelObj = null;
                    }

                    Bootstrap.Instance.UIManager.Push(UISystem.ScreenID.MainMenu);

                    break;
                case GameState.WaitingToStart:
                    Bootstrap.Instance.UIManager.Push(UISystem.ScreenID.ActionPhase);

                    _playerObj = Object.Instantiate(Bootstrap.Instance.PlayerPrefab);
                    _levelObj = Object.Instantiate(Bootstrap.Instance.LevelOnePrefab);

                    Bootstrap.Instance.UIManager.Push(UISystem.ScreenID.Tutorial);
                    break;
                case GameState.GamePlaying:
                    _countdownToStartTimer = COUNTDOWN_TO_START_TIMER_MAX;
                    break;
                case GameState.GameOver:
                    _gameplayingTimer = 0;
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
