using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class DeliveryResultUI : MonoBehaviour
{
    [Header("Asset Ref")]
    [SerializeField] private Sprite _successSprite;
    [SerializeField] private Sprite _failedSprite;

    [Header("Internal Ref")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _backgroundImg;
    [SerializeField] private Image _iconImg;
    [SerializeField] private TextMeshProUGUI _messageTxt;

    [Header("Property")]
    [SerializeField] private Color _successColor;
    [SerializeField] private Color _failedColor;

    private readonly int _popupHashKey = Animator.StringToHash("Popup");

    private void Start()
    {
        Bootstrap.Instance.EventMgr.DeliverReceiptSuccess += OnDeliverReceiptSuccess;
        Bootstrap.Instance.EventMgr.DeliverReceiptFailed += OnDeliverReceiptFailed;

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Bootstrap.Instance.EventMgr.DeliverReceiptSuccess -= OnDeliverReceiptSuccess;
        Bootstrap.Instance.EventMgr.DeliverReceiptFailed -= OnDeliverReceiptFailed;
    }

    private void OnDeliverReceiptSuccess()
    {
        _backgroundImg.color = _successColor;
        _iconImg.sprite = _successSprite;
        _messageTxt.SetText("DELIVERY\nSUCCESS");

        _animator.SetTrigger(_popupHashKey);
        gameObject.SetActive(true);
    }

    private void OnDeliverReceiptFailed()
    {
        _backgroundImg.color = _failedColor;
        _iconImg.sprite = _failedSprite;
        _messageTxt.SetText("DELIVERY\nFAILED");

        _animator.SetTrigger(_popupHashKey);
        gameObject.SetActive(true);
    }
}
