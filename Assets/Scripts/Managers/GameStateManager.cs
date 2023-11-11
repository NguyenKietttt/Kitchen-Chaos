using UnityEngine;

public sealed class GameStateManager
{
    public enum State { Loading, WaitingToStart, CountDownToStart, GamePlaying, GameOver }

    private const float COUNTDOWN_TO_START_TIMER_MAX = 3.0f;
    private const float GAME_PLAYING_TIMER_MAX = 60.0f;

    public float GamePlayingTimerNormalized => _gameplayingTimer / GAME_PLAYING_TIMER_MAX;
    public float CountDownToStartTimer => _countdownToStartTimer;
    public bool IsGamePlaying => _curState is State.GamePlaying;
    public bool IsCounDownToStartActive => _curState is State.CountDownToStart;
    public bool IsGameOver => _curState is State.GameOver;

    private State _curState;
    private float _countdownToStartTimer = COUNTDOWN_TO_START_TIMER_MAX;
    private float _gameplayingTimer;

    public GameStateManager()
    {
        _curState = State.Loading;

        Bootstrap.Instance.EventMgr.TooglePause += OnTogglePaused;
        Bootstrap.Instance.EventMgr.Interact += OnInteract;
    }

    public void Init()
    {
        _curState = State.WaitingToStart;
    }

    public void OnUpdate(in float deltaTime)
    {
        switch (_curState)
        {
            case State.CountDownToStart:
                _countdownToStartTimer -= deltaTime;
                if (_countdownToStartTimer < 0)
                {
                    _countdownToStartTimer = COUNTDOWN_TO_START_TIMER_MAX;
                    _curState = State.GamePlaying;

                    Bootstrap.Instance.EventMgr.ChangeGameState?.Invoke();
                }
                break;
            case State.GamePlaying:
                _gameplayingTimer += Time.deltaTime;
                if (_gameplayingTimer >= GAME_PLAYING_TIMER_MAX)
                {
                    _gameplayingTimer = 0;
                    _curState = State.GameOver;

                    Bootstrap.Instance.EventMgr.ChangeGameState?.Invoke();
                }
                break;
        }
    }

    public void OnDestroy()
    {
        Bootstrap.Instance.EventMgr.TooglePause -= OnTogglePaused;
        Bootstrap.Instance.EventMgr.Interact -= OnInteract;
    }

    public void Reset()
    {
        _curState = State.Loading;

        _countdownToStartTimer = COUNTDOWN_TO_START_TIMER_MAX;
        _gameplayingTimer = 0;
    }

    private void OnInteract()
    {
        if (_curState is State.WaitingToStart)
        {
            _curState = State.CountDownToStart;
            Bootstrap.Instance.EventMgr.ChangeGameState?.Invoke();
        }
    }

    private void OnTogglePaused()
    {
        if (_curState is State.Loading)
        {
            return;
        }

        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            Bootstrap.Instance.EventMgr.OnPaused?.Invoke();
        }
        else
        {
            Time.timeScale = 1;
            Bootstrap.Instance.EventMgr.OnUnPaused?.Invoke();
        }
    }
}
