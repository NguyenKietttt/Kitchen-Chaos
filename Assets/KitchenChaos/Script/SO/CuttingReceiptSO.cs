using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "NewCuttingReceiptSO", menuName = "Scriptable Object/Cutting Receipt")]
    public sealed class CuttingReceiptSO : ScriptableObject
    {
        public KitchenObjectSO Input => _input;
        public KitchenObjectSO Output => _output;
        public int CuttingProcessMax => _cuttingProcessMax;

        [Header("Asset Ref")]
        [SerializeField] private KitchenObjectSO _input;
        [SerializeField] private KitchenObjectSO _output;

        [Header("Property")]
        [SerializeField, Min(1)] private int _cuttingProcessMax;
    }
}
