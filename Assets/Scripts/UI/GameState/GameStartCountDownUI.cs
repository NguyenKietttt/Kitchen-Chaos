using TMPro;
using UnityEngine;

public sealed class GameStartCountDownUI : MonoBehaviour
{
    [Header("Internal Ref")]
    [SerializeField] private TextMeshProUGUI _countDownTxt;
    [SerializeField] private Animator _animator;

    private readonly int _countdownNumberKeyHash = Animator.StringToHash("CountdownNumber_Key");

    private int _preCountdownNumber;

    private void Start()
    {
        Bootstrap.Instance.EventMgr.ChangeGameState += OnGameStateChanged;
        Hide();
    }

    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(Bootstrap.Instance.GameStateMgr.CountDownToStartTimer);
        _countDownTxt.SetText(countdownNumber.ToString());

        if (_preCountdownNumber != countdownNumber)
        {
            _preCountdownNumber = countdownNumber;
            _animator.SetTrigger(_countdownNumberKeyHash);

            Bootstrap.Instance.EventMgr.CountdownPopup?.Invoke();
        }
    }

    private void OnDestroy()
    {
        Bootstrap.Instance.EventMgr.ChangeGameState -= OnGameStateChanged;
    }

    private void OnGameStateChanged()
    {
        if (Bootstrap.Instance.GameStateMgr.IsCounDownToStartActive)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
