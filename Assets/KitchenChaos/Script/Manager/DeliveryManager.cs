using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class DeliveryManager
    {
        private const float SPAWN_RECEIPT_TIMER_MAX = 4.0f;
        private const float SPAWN_RECEIPT_TIMER_MIN = 0.0f;
        private const int WAITING_RECEIPT_MAX = 4;
        private const int DEFAULT_AMOUNT_SUCCESSFUL_RECEIPT = 0;

        public IReadOnlyList<DishReceiptSO> WaitingReceiptsSO => _waitingReceiptsSO.AsReadOnly();
        public int AmountSucessfulReceipt => _amountSucessfulReceipt;

        private readonly List<DishReceiptSO> _waitingReceiptsSO = new();
        private readonly DishReceiptsSO _dishReceiptsS0;

        private GameState _curState;
        private float _spawnReceiptTimer;
        private int _amountSucessfulReceipt;

        public DeliveryManager(DishReceiptsSO dishReceiptsSO)
        {
            _dishReceiptsS0 = dishReceiptsSO;
            Bootstrap.Instance.EventMgr.ChangeGameState += OnGameStateChanged;
        }

        public void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.ChangeGameState -= OnGameStateChanged;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_curState is not GameState.GamePlaying)
            {
                return;
            }

            _spawnReceiptTimer += deltaTime;

            if (_spawnReceiptTimer >= SPAWN_RECEIPT_TIMER_MAX)
            {
                _spawnReceiptTimer = SPAWN_RECEIPT_TIMER_MIN;

                if (_curState is GameState.GamePlaying && _waitingReceiptsSO.Count < WAITING_RECEIPT_MAX)
                {
                    DishReceiptSO waitingReceiptSO = _dishReceiptsS0.Receipts[Random.Range(0, _dishReceiptsS0.Receipts.Count)];
                    _waitingReceiptsSO.Add(waitingReceiptSO);

                    Bootstrap.Instance.EventMgr.SpawnReceipt?.Invoke();
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
                        _amountSucessfulReceipt++;
                        _waitingReceiptsSO.RemoveAt(i);

                        Bootstrap.Instance.EventMgr.CompleteReceipt?.Invoke();
                        Bootstrap.Instance.EventMgr.DeliverReceiptSuccess?.Invoke();

                        return;
                    }
                }
            }

            Bootstrap.Instance.EventMgr.DeliverReceiptFailed?.Invoke();
        }

        public void Reset()
        {
            _waitingReceiptsSO.Clear();

            _spawnReceiptTimer = SPAWN_RECEIPT_TIMER_MIN;
            _amountSucessfulReceipt = DEFAULT_AMOUNT_SUCCESSFUL_RECEIPT;
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
    }
}
