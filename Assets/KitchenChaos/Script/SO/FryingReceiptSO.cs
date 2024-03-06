using KitchenChaos.Utils;
using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "FryingReceiptSO", menuName = "Scriptable Object/Frying Receipt")]
    public sealed class FryingReceiptSO : ScriptableObject
    {
        public KitchenObjectSO Input => _input!;
        public KitchenObjectSO Output => _output!;
        public float FryingTimeMax => _fryingTimeMax;

        [Header("Asset Ref")]
        [SerializeField] private KitchenObjectSO? _input;
        [SerializeField] private KitchenObjectSO? _output;

        [Header("Property")]
        [SerializeField] private float _fryingTimeMax;

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
