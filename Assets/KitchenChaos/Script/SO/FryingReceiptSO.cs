using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "FryingReceiptSO", menuName = "ScriptableObject/FryingReceipt")]
    public sealed class FryingReceiptSO : ScriptableObject
    {
        public KitchenObjectSO Input => _input;
        public KitchenObjectSO Output => _output;
        public float FryingTimeMax => _fryingTimeMax;

        [Header("Asset Ref")]
        [SerializeField] private KitchenObjectSO _input;
        [SerializeField] private KitchenObjectSO _output;

        [Header("Property")]
        [SerializeField] private float _fryingTimeMax;
    }
}
