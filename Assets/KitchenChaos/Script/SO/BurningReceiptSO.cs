using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "SO_BurningReceipt", menuName = "ScriptableObject/BurningReceipt")]
    public sealed class BurningReceiptSO : ScriptableObject
    {
        public KitchenObjectSO Input => _input;
        public KitchenObjectSO Output => _output;
        public float BurningTimeMax => _burningTimeMax;

        [Header("Asset Ref")]
        [SerializeField] private KitchenObjectSO _input;
        [SerializeField] private KitchenObjectSO _output;

        [Header("Property")]
        [SerializeField] private float _burningTimeMax;
    }
}
