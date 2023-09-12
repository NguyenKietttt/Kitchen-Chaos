using System.Collections.Generic;
using UnityEngine;

public sealed class DeliveryManager
{
    private const string LIST_RECEIPT_SO_PATH = "ScriptableObjects/SO_ListReceipt";
    private const float SPAWN_RECEIPT_TIMER_MAX = 4.0f;
    private const int WAITING_RECEIPT_MAX = 4;

    private readonly List<ReceiptSO> _waitingListReceiptSO = new();
    private readonly ListReceiptSO _receiptSOList;

    private float _spawnReceiptTimer;

    public DeliveryManager()
    {
        _receiptSOList = Resources.Load<ListReceiptSO>(LIST_RECEIPT_SO_PATH);
    }

    public void OnUpdate()
    {
        _spawnReceiptTimer += Time.deltaTime;

        if (_spawnReceiptTimer >= SPAWN_RECEIPT_TIMER_MAX)
        {
            _spawnReceiptTimer = 0;

            if (_waitingListReceiptSO.Count < WAITING_RECEIPT_MAX)
            {
                ReceiptSO waitingReceiptSO = _receiptSOList.ReceiptSOList[Random.Range(0, _receiptSOList.ReceiptSOList.Length)];
                _waitingListReceiptSO.Add(waitingReceiptSO);
                Debug.Log("==== " + waitingReceiptSO.ReceiptName);
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
                    Debug.Log("==== Player delivered the correct receipt!!!");

                    _waitingListReceiptSO.RemoveAt(i);
                    return;
                }
            }
        }

        Debug.Log("==== Icorrect receipt!!!");
    }
}
