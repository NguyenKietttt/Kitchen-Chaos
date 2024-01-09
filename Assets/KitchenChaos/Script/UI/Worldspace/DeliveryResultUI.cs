using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class DeliveryResultUI : MonoBehaviour
{
    [Header("Asset Ref")]
    [SerializeField] private Sprite _successSprite;
    [SerializeField] private Sprite _failedSprite;

    [Header("Internal Ref")]
    [SerializeField] private Image _backgroundImg;
    [SerializeField] private Image _iconImg;
    [SerializeField] private TextMeshProUGUI _messageTxt;
    [SerializeField] private CanvasGroup _canvasGroup;

    [Header("Property")]
    [SerializeField] private Color _successColor;
    [SerializeField] private Color _failedColor;

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

        PlayShowingSequence();
        gameObject.SetActive(true);
    }

    private void OnDeliverReceiptFailed()
    {
        _backgroundImg.color = _failedColor;
        _iconImg.sprite = _failedSprite;
        _messageTxt.SetText("DELIVERY\nFAILED");

        PlayShowingSequence();
        gameObject.SetActive(true);
    }

    private void PlayShowingSequence()
    {
        // Scale
        DOTween.Sequence()
            .AppendCallback(() => transform.localScale = new Vector3(0.6f, 0.6f, 0.6f))
            .Append(transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.025f))
            .Append(transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f))
            .Append(transform.DOScale(Vector3.one, 1.0f));

        // Rotate
        DOTween.Sequence()
            .AppendCallback(() => transform.eulerAngles = new Vector3(0.0f, 0.0f, 15.0f))
            .Append(transform.DOLocalRotate(new Vector3(0.0f, 0.0f, -15.0f), 0.025f))
            .Append(transform.DOLocalRotate(Vector3.zero, 0.01f));

        // Fade
        DOTween.Sequence()
            .AppendCallback(() => _canvasGroup.alpha = 1.0f)
            .Append(_canvasGroup.DOFade(1.0f, 0.5f))
            .Append(_canvasGroup.DOFade(0.0f, 0.5f));
    }
}
