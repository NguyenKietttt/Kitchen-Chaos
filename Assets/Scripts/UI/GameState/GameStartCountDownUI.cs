using TMPro;
using UnityEngine;

public sealed class GameStartCountDownUI : MonoBehaviour
{
    [Header("Internal Ref")]
    [SerializeField] private TextMeshProUGUI _countDownTxt;

    private void Start()
    {
        Bootstrap.Instance.EventMgr.ChangeGameState += OnGameStateChanged;
        Hide();
    }

    private void Update()
    {
        _countDownTxt.SetText(Mathf.Ceil(Bootstrap.Instance.GameStateMgr.CountDownToStartTimer).ToString());
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
