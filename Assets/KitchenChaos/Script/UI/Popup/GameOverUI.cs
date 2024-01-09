using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;

public sealed class GameOverUI : BaseScreen
{
    [Header("Internal Ref")]
    [SerializeField] private TextMeshProUGUI _amountReceiptDeliveredTxt;
    [SerializeField] private Button _mainMenuBtn;

    public override void OnPush(object[] datas = null)
    {
        Bootstrap.Instance.EventMgr.ChangeGameState += OnGameStateChanged;
        _mainMenuBtn.onClick.AddListener(() => OnMainMenuBtnClicked());
    }

    public override void OnPop()
    {
        _mainMenuBtn.onClick.RemoveAllListeners();
        Bootstrap.Instance.EventMgr.ChangeGameState -= OnGameStateChanged;

        Destroy(gameObject);
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
