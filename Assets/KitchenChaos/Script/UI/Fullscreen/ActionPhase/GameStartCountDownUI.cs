using DG.Tweening;
using TMPro;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class GameStartCountDownUI : MonoBehaviour
    {
        [Header("Internal Ref")]
        [SerializeField] private TextMeshProUGUI _countDownTxt;
        [SerializeField] private CanvasGroup _canvasGroup;

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
                PlayShowingSequence();

                Bootstrap.Instance.EventMgr.CountdownPopup?.Invoke();
            }
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.ChangeGameState -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            if (state is GameState.CountDownToStart)
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

        private void PlayShowingSequence()
        {
            // Scale
            DOTween.Sequence()
                .AppendCallback(() => transform.localScale = new Vector3(0.6f, 0.6f, 0.6f))
                .Append(transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.025f))
                .Append(transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.075f))
                .Append(transform.DOScale(Vector3.one, 0.9f));

            // Rotate
            DOTween.Sequence()
                .AppendCallback(() => transform.eulerAngles = new Vector3(0.0f, 0.0f, 17.0f))
                .Append(transform.DOLocalRotate(new Vector3(0.0f, 0.0f, -17.0f), 0.025f))
                .Append(transform.DOLocalRotate(Vector3.zero, 0.075f));

            // Fade
            DOTween.Sequence()
                .AppendCallback(() => _canvasGroup.alpha = 1.0f)
                .Append(_canvasGroup.DOFade(1.0f, 0.5f))
                .Append(_canvasGroup.DOFade(0.0f, 0.5f));
        }
    }
}
