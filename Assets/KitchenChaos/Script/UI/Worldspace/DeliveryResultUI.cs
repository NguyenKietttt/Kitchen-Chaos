using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    public sealed class DeliveryResultUI : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private DeliveryResultUICfg _config;

        [Header("Internal Ref")]
        [SerializeField] private Image _backgroundImg;
        [SerializeField] private Image _iconImg;
        [SerializeField] private TextMeshProUGUI _messageTxt;
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Start()
        {
            Bootstrap.Instance.EventMgr.DeliverReceiptSuccess += OnDeliverReceiptSuccess;
            Bootstrap.Instance.EventMgr.DeliverReceiptFailed += OnDeliverReceiptFailed;

            Hide();
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.DeliverReceiptSuccess -= OnDeliverReceiptSuccess;
            Bootstrap.Instance.EventMgr.DeliverReceiptFailed -= OnDeliverReceiptFailed;
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnDeliverReceiptSuccess()
        {
            UpdateNotiResult(_config.SuccessColor, _config.SuccessSpr, "DELIVERY\nSUCCESS");
            PlayShowingSequence();
            Show();
        }

        private void OnDeliverReceiptFailed()
        {
            UpdateNotiResult(_config.FailedColor, _config.FailedSpr, "DELIVERY\nFAILED");
            PlayShowingSequence();
            Show();
        }

        private void UpdateNotiResult(Color bgColor, Sprite iconSpr, string message)
        {
            _backgroundImg.color = bgColor;
            _iconImg.sprite = iconSpr;
            _messageTxt.SetText(message);
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
}
