using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class DeliveryManager : MonoBehaviour
    {
        public IReadOnlyList<DishReceiptSO> WaitingReceiptsSO => _waitingReceiptsSO.AsReadOnly();
        public int AmountSucessfulReceipt => _amountSucessfulReceipt;

        private readonly List<DishReceiptSO> _waitingReceiptsSO = new();

        [Header("Config")]
        [SerializeField] private DeliveryManagerCfg _config;

        private GameState _curState;
        private float _spawnReceiptTimer;
        private int _amountSucessfulReceipt;

        public void Init()
        {
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

            if (_spawnReceiptTimer >= _config.SpawnReceiptTimerMax)
            {
                _spawnReceiptTimer = _config.SpawnReceiptTimerMin;

                if (_curState is GameState.GamePlaying && _waitingReceiptsSO.Count < _config.WaitingReceiptMax)
                {
                    int index = Random.Range(0, _config.DishReceiptsS0.Receipts.Count);
                    DishReceiptSO waitingReceiptSO = _config.DishReceiptsS0.Receipts[index];
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

        private void Reset()
        {
            _waitingReceiptsSO.Clear();

            _spawnReceiptTimer = _config.SpawnReceiptTimerMin;
            _amountSucessfulReceipt = _config.WaitingReceiptMin;
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
