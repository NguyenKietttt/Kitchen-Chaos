using UnityEngine;

[CreateAssetMenu(fileName = "SO_ListReceipt", menuName = "ScriptableObject/ListReceipt")]
public sealed class ListReceiptSO : ScriptableObject
{
    public ReceiptSO[] ReceiptSOList => _receiptSOList;

    [SerializeField] private ReceiptSO[] _receiptSOList;
}
