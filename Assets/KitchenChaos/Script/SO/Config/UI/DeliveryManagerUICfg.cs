using KitchenChaos.Utils;
using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_DeliveryManagerUI", menuName = "Scriptable Object/Config/UI/Delivery Manager UI")]
    public sealed class DeliveryManagerUICfg : ScriptableObject
    {
        public DeliveryManagerSingleUI ReceiptTemplateUI => _receiptTemplateUI!;

        [Header("Asset Ref")]
        [SerializeField] private DeliveryManagerSingleUI? _receiptTemplateUI;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void CheckNullEditorReferences()
        {
            if (_receiptTemplateUI == null)
            {
                CustomLog.LogError(this, "missing references in editor!!!");
            }
        }
    }
}
