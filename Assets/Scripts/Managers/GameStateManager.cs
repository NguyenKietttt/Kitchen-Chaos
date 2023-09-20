public sealed class GameStateManager
{
    public enum State { Loading, WaitingToStart, CountDownToStart, GamePlaying, GameOver }

    private const float WAITING_TO_START_TIMER_MAX = 1.0f;
    private const float COUNTDOWN_TO_START_TIMER_MAX = 3.0f;

    private State _curState;
    private float _waitingToStartTimer;
    private float _countdownToStartTimer = COUNTDOWN_TO_START_TIMER_MAX;

    public GameStateManager()
    {
        _curState = State.Loading;
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
        }
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
}
