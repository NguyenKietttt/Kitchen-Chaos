using UnityEngine;

[CreateAssetMenu(fileName = "NewCuttingReceiptSO", menuName = "ScriptableObject/CuttingReceipt")]
public sealed class CuttingReceiptSO : ScriptableObject
{
    public KitchenObjectSO Input => _input;
    public KitchenObjectSO Output => _output;
    public int CuttingProcessMax => _cuttingProcessMax;

    [Header("Asset Ref")]
    [SerializeField] private KitchenObjectSO _input;
    [SerializeField] private KitchenObjectSO _output;

    [Header("Property")]
    [SerializeField] private int _cuttingProcessMax = 1;
}
