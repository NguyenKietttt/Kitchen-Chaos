using System.Collections.Generic;
using UnityEngine;

public sealed class DeliveryManager
{
    private const float SPAWN_RECEIPT_TIMER_MAX = 4.0f;
    private const int WAITING_RECEIPT_MAX = 4;

    public IEnumerable<ReceiptSO> ListWaitingReceiptSO => _waitingListReceiptSO.AsReadOnly();
    public int AmountSucessfulReceipt => _amountSucessfulReceipt;

    private readonly List<ReceiptSO> _waitingListReceiptSO = new();
    private readonly ListReceiptSO _receiptSOList;

    private GameState _curState;
    private float _spawnReceiptTimer;
    private int _amountSucessfulReceipt;

    public DeliveryManager(ListReceiptSO listReceiptSO)
    {
        _receiptSOList = listReceiptSO;
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
            _spawnReceiptTimer = 0;

            if (_curState is GameState.GamePlaying && _waitingListReceiptSO.Count < WAITING_RECEIPT_MAX)
            {
                ReceiptSO waitingReceiptSO = _receiptSOList.ReceiptSOList[Random.Range(0, _receiptSOList.ReceiptSOList.Length)];
                _waitingListReceiptSO.Add(waitingReceiptSO);

                Bootstrap.Instance.EventMgr.SpawnReceipt?.Invoke();
            }
        }
    }

    public void DeliveryReceipt(PlateKitchenObject plateKitchenObj)
    {
        for (int i = 0; i < _waitingListReceiptSO.Count; i++)
        {
            ReceiptSO waitingReceiptSO = _waitingListReceiptSO[i];
            HashSet<KitchenObjectSO> listPlateKichenObjSO = plateKitchenObj.GetListKitchenObjectSO();

            if (waitingReceiptSO.ListKitchenObjSO.Length == listPlateKichenObjSO.Count)
            {
                bool isPlateContentMatchesReceipt = true;

                foreach (KitchenObjectSO receiptKitchenObjSO in waitingReceiptSO.ListKitchenObjSO)
                {
                    bool isIngredientFound = false;

                    if (listPlateKichenObjSO.Contains(receiptKitchenObjSO))
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
                    _waitingListReceiptSO.RemoveAt(i);

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
        _waitingListReceiptSO.Clear();

        _spawnReceiptTimer = 0;
        _amountSucessfulReceipt = 0;
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
