using UnityEngine;

public sealed class GameStateManager
{
    public enum State { Loading, WaitingToStart, CountDownToStart, GamePlaying, GameOver }

    private const float WAITING_TO_START_TIMER_MAX = 1.0f;
    private const float COUNTDOWN_TO_START_TIMER_MAX = 3.0f;
    private const float GAME_PLAYING_TIMER_MAX = 60.0f;

    private State _curState;
    private float _waitingToStartTimer;
    private float _countdownToStartTimer = COUNTDOWN_TO_START_TIMER_MAX;
    private float _gameplayingTimer;

    public GameStateManager()
    {
        _curState = State.Loading;
        Bootstrap.Instance.EventMgr.TooglePause += OnTogglePaused;
    }

    public void Init()
    {
        _curState = State.WaitingToStart;
    }

    public void OnUpdate(in float deltaTime)
    {
        switch (_curState)
        {
            case State.WaitingToStart:
                _waitingToStartTimer += deltaTime;
                if (_waitingToStartTimer >= WAITING_TO_START_TIMER_MAX)
                {
                    _waitingToStartTimer = 0;
                    _curState = State.CountDownToStart;

                    Bootstrap.Instance.EventMgr.ChangeGameState?.Invoke();
                }
                break;
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
    }

    public float GetCountDownToStartTimer()
    {
        return _countdownToStartTimer;
    }

    public bool IsGamePlaying()
    {
        return _curState is State.GamePlaying;
    }

    public bool IsCounDownToStartActive()
    {
        return _curState is State.CountDownToStart;
    }

    public bool IsGameOver()
    {
        return _curState is State.GameOver;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return _gameplayingTimer / GAME_PLAYING_TIMER_MAX;
    }

    public void Reset()
    {
        _curState = State.Loading;

        _waitingToStartTimer = 0;
        _countdownToStartTimer = COUNTDOWN_TO_START_TIMER_MAX;
        _gameplayingTimer = 0;
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
