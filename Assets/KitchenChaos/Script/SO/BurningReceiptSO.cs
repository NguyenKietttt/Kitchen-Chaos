using KitchenChaos.Utils;
using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "SO_BurningReceipt", menuName = "Scriptable Object/Burning Receipt")]
    public sealed class BurningReceiptSO : ScriptableObject
    {
        public KitchenObjectSO Input => _input!;
        public KitchenObjectSO Output => _output!;
        public float BurningTimeMax => _burningTimeMax;

        [Header("Asset Ref")]
        [SerializeField] private KitchenObjectSO? _input;
        [SerializeField] private KitchenObjectSO? _output;

        [Header("Property")]
        [SerializeField] private float _burningTimeMax;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void CheckNullEditorReferences()
        {
            if (_input == null || _output == null)
            {
                CustomLog.LogError(this, "missing references in editor!!!");
            }
        }
    }
}
