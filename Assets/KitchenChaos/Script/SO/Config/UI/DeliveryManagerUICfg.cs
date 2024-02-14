using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_DeliveryManagerUI", menuName = "Scriptable Object/Config/UI/Delivery Manager UI")]
    public sealed class DeliveryManagerUICfg : ScriptableObject
    {
        public DeliveryManagerSingleUI ReceiptTemplateUI => _receiptTemplateUI;

        [Header("Asset Ref")]
        [SerializeField] private DeliveryManagerSingleUI _receiptTemplateUI;
    }
}
