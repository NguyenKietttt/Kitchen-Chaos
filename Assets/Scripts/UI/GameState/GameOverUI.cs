using TMPro;
using UnityEngine;

public sealed class GameOverUI : MonoBehaviour
{
    [Header("Internal Ref")]
    [SerializeField] private TextMeshProUGUI _amountReceiptDeliveredTxt;

    private void Start()
    {
        Bootstrap.Instance.EventMgr.ChangeGameState += OnGameStateChanged;
        Hide();
    }

    private void OnDestroy()
    {
        Bootstrap.Instance.EventMgr.ChangeGameState -= OnGameStateChanged;
    }

    private void OnGameStateChanged()
    {
        if (Bootstrap.Instance.GameStateMgr.IsGameOver)
        {
            Show();
            _amountReceiptDeliveredTxt.SetText(Bootstrap.Instance.DeliveryMgr.AmountSucessfulReceipt.ToString());
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
