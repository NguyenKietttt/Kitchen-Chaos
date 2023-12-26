using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class GameOverUI : MonoBehaviour
{
    [Header("Internal Ref")]
    [SerializeField] private TextMeshProUGUI _amountReceiptDeliveredTxt;
    [SerializeField] private Button _mainMenuBtn;

    private void Awake()
    {
        Bootstrap.Instance.EventMgr.ChangeGameState += OnGameStateChanged;
        _mainMenuBtn.onClick.AddListener(() => OnMainMenuBtnClicked());
    }

    private void Start()
    {
        Hide();
    }

    private void OnDestroy()
    {
        _mainMenuBtn.onClick.RemoveAllListeners();
        Bootstrap.Instance.EventMgr.ChangeGameState -= OnGameStateChanged;
    }

    private void OnMainMenuBtnClicked()
    {
        Bootstrap.Instance.GameStateMgr.ChangeState(GameState.MainMenu);
    }

    private void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.GameOver:
                _amountReceiptDeliveredTxt.SetText(Bootstrap.Instance.DeliveryMgr.AmountSucessfulReceipt.ToString());
                Show();
                break;
            default:
                Hide();
                break;
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
