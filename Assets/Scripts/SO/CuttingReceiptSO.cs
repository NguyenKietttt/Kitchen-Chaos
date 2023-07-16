using UnityEngine;

[CreateAssetMenu(fileName = "NewCuttingReceiptSO", menuName = "ScriptableObject/CuttingReceipt")]
public sealed class CuttingReceiptSO : ScriptableObject
{
    public KitchenObjectSO Input => _input;
    public KitchenObjectSO Output => _output;

    [SerializeField] private KitchenObjectSO _input;
    [SerializeField] private KitchenObjectSO _output;
}