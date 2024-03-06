using KitchenChaos.Utils;
using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class GameOverUI : BaseScreen
    {
        [Header("Internal Ref")]
        [SerializeField] private TextMeshProUGUI? _amountReceiptDeliveredTxt;
        [SerializeField] private Button? _mainMenuBtn;

        private EventManager? _eventMgr;
        private GameStateManager? _gameStateMgr;
        private DeliveryManager? _deliveryMgr;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        public override void OnPush(object[]? datas = null)
        {
            RegisterServices();
            SubscribeEvents();
        }

        public override void OnPop()
        {
            UnsubscribeEvents();
            DeregisterServices();

            Destroy(gameObject);
        }

        private void OnMainMenuBtnClicked()
        {
            _gameStateMgr!.ChangeState(GameState.MainMenu);
        }

        private void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.GameOver:
                    string AmountSuccessfulReceiptString = _deliveryMgr!.AmountSuccessfulReceipt.ToString();
                    _amountReceiptDeliveredTxt!.SetText(AmountSuccessfulReceiptString);
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

        private void CheckNullEditorReferences()
        {
            if (_amountReceiptDeliveredTxt == null || _mainMenuBtn == null)
            {
                CustomLog.LogError(this, "missing references in editor!!!");
            }
        }

        private void RegisterServices()
        {
            _eventMgr = ServiceLocator.Instance.Get<EventManager>();
            _gameStateMgr = ServiceLocator.Instance.Get<GameStateManager>();
            _deliveryMgr = ServiceLocator.Instance.Get<DeliveryManager>();
        }

        private void DeregisterServices()
        {
            _eventMgr = null;
            _gameStateMgr = null;
            _deliveryMgr = null;
        }

        private void SubscribeEvents()
        {
            _mainMenuBtn!.onClick.AddListener(() => OnMainMenuBtnClicked());
            _eventMgr!.ChangeGameState += OnGameStateChanged;
        }

        private void UnsubscribeEvents()
        {
            _mainMenuBtn!.onClick.RemoveAllListeners();
            _eventMgr!.ChangeGameState -= OnGameStateChanged;
        }
    }
}
