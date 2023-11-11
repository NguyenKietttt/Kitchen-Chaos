using UnityEngine;

[CreateAssetMenu(fileName = "SO_Receipt", menuName = "ScriptableObject/Receipt")]
public sealed class ReceiptSO : ScriptableObject
{
    public KitchenObjectSO[] ListKitchenObjSO => _listKitchenObjSO;
    public string ReceiptName => _receiptName;

    [SerializeField] private KitchenObjectSO[] _listKitchenObjSO;
    [SerializeField] private string _receiptName;
}
