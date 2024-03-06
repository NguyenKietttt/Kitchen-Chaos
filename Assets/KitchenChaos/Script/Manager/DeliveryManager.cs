using System.Collections.Generic;
using System.Linq;
using KitchenChaos.Utils;
using UnityEngine;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class DeliveryManager : MonoBehaviour
    {
        public IReadOnlyList<DishReceiptSO> WaitingReceiptsSO => _waitingReceiptsSO.AsReadOnly();
        public int AmountSuccessfulReceipt => _amountSuccessfulReceipt;

        private readonly List<DishReceiptSO> _waitingReceiptsSO = new();

        [Header("Config")]
        [SerializeField] private DeliveryManagerCfg? _config;

        private EventManager? _eventMgr;

        private GameState _curState;
        private float _spawnReceiptTimer;
        private int _amountSuccessfulReceipt;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        public void Init()
        {
            RegisterServices();
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
            DeregisterServices();
        }

        private void Update()
        {
            if (_curState is not GameState.GamePlaying)
            {
                return;
            }

            _spawnReceiptTimer += Time.deltaTime;

            if (_spawnReceiptTimer >= _config!.SpawnReceiptTimerMax)
            {
                _spawnReceiptTimer = _config.SpawnReceiptTimerMin;

                if (_curState is GameState.GamePlaying && _waitingReceiptsSO.Count < _config.WaitingReceiptMax)
                {
                    int index = Random.Range(0, _config.DishReceiptsS0.Receipts.Count);
                    DishReceiptSO waitingReceiptSO = _config.DishReceiptsS0.Receipts[index];
                    _waitingReceiptsSO.Add(waitingReceiptSO);

                    _eventMgr!.SpawnReceipt?.Invoke();
                }
            }
        }

        public void DeliveryReceipt(PlateKitchenObject plateKitchenObj)
        {
            for (int i = 0; i < _waitingReceiptsSO.Count; i++)
            {
                DishReceiptSO waitingReceiptSO = _waitingReceiptsSO[i];
                IReadOnlyCollection<KitchenObjectSO> kitchenObjHashSet = plateKitchenObj.KitchenObjHashSet;

                if (waitingReceiptSO.KitchenObjsSO.Count == kitchenObjHashSet.Count)
                {
                    bool isPlateContentMatchesReceipt = true;

                    foreach (KitchenObjectSO receiptKitchenObjSO in waitingReceiptSO.KitchenObjsSO)
                    {
                        bool isIngredientFound = false;

                        if (kitchenObjHashSet.Contains(receiptKitchenObjSO))
                        {
                            isIngredientFound = true;
                        }

                        if (!isIngredientFound)
                        {
                            isPlateContentMatchesReceipt = false;
                        }
                    }

                    if (isPlateContentMatchesReceipt)
                    {
                        _amountSuccessfulReceipt++;
                        _waitingReceiptsSO.RemoveAt(i);

                        _eventMgr!.CompleteReceipt?.Invoke();
                        _eventMgr.DeliverReceiptSuccess?.Invoke();

                        return;
                    }
                }
            }

            _eventMgr!.DeliverReceiptFailed?.Invoke();
        }

        private void Reset()
        {
            _waitingReceiptsSO.Clear();

            _spawnReceiptTimer = _config!.SpawnReceiptTimerMin;
            _amountSuccessfulReceipt = _config.WaitingReceiptMin;
        }

        private void OnGameStateChanged(GameState state)
        {
            _curState = state;

            switch (_curState)
            {
                case GameState.MainMenu:
                    Reset();
                    break;
            }
        }

        private void CheckNullEditorReferences()
        {
            if (_config == null)
            {
                CustomLog.LogError(this, "missing internal references in editor!");
            }
        }

        private void RegisterServices()
        {
            _eventMgr = ServiceLocator.Instance.Get<EventManager>();
        }

        private void DeregisterServices()
        {
            _eventMgr = null;
        }

        private void SubscribeEvents()
        {
            _eventMgr!.ChangeGameState += OnGameStateChanged;
        }

        private void UnsubscribeEvents()
        {
            _eventMgr!.ChangeGameState -= OnGameStateChanged;
        }
    }
}
