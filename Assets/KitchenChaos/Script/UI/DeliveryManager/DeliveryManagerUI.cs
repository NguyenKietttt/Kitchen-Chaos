using System.Collections.Generic;
using UnityEngine;

public sealed class DeliveryManagerUI : MonoBehaviour
{
    [Header("Asset Ref")]
    [SerializeField] private DeliveryManagerSingleUI _receiptTemplateUI;

    [Header("Internal Ref")]
    [SerializeField] private Transform _container;

    private void Awake()
    {
        _receiptTemplateUI.gameObject.SetActive(false);
    }

    private void Start()
    {
        Bootstrap.Instance.EventMgr.SpawnReceipt += UpdateVisual;
        Bootstrap.Instance.EventMgr.CompleteReceipt += UpdateVisual;

        UpdateVisual();
    }

    private void OnDestroy()
    {
        Bootstrap.Instance.EventMgr.SpawnReceipt -= UpdateVisual;
        Bootstrap.Instance.EventMgr.CompleteReceipt -= UpdateVisual;
    }

    public void UpdateVisual()
    {
        ClearPreviousVisual();

        List<ReceiptSO> listWaitingReceiptSO = Bootstrap.Instance.DeliveryMgr.GetListWaitingReceiptSO();

        for (int i = 0; i < listWaitingReceiptSO.Count; i++)
        {
            ReceiptSO receiptSO = listWaitingReceiptSO[i];

            DeliveryManagerSingleUI waitingReceipUI = Instantiate(_receiptTemplateUI, _container);
            waitingReceipUI.SetReceiptName(listWaitingReceiptSO[i].ReceiptName);
            waitingReceipUI.SetIngredientIcons(receiptSO.ListKitchenObjSO);

            waitingReceipUI.gameObject.SetActive(true);
        }
    }

    private void ClearPreviousVisual()
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }
    }
}
