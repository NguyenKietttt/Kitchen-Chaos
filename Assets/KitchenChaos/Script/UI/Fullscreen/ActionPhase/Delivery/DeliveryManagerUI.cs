using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class DeliveryManagerUI : MonoBehaviour
    {
        [Header("Asset Ref")]
        [SerializeField] private DeliveryManagerSingleUI _receiptTemplateUI;

        [Header("Internal Ref")]
        [SerializeField] private Transform _container;

        private void Awake()
        {
            _receiptTemplateUI.Hide();
        }

        private void Start()
        {
            Bootstrap.Instance.EventMgr.SpawnReceipt += UpdateVisual;
            Bootstrap.Instance.EventMgr.CompleteReceipt += UpdateVisual;

            UpdateVisual();
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.SpawnReceipt -= UpdateVisual;
            Bootstrap.Instance.EventMgr.CompleteReceipt -= UpdateVisual;
        }

        public void UpdateVisual()
        {
            ClearPreviousVisual();

            IEnumerable<DishReceiptSO> waitingDishReceipts = Bootstrap.Instance.DeliveryMgr.WaitingReceiptsSO;
            foreach (DishReceiptSO receipt in waitingDishReceipts)
            {
                DeliveryManagerSingleUI waitingReceipUI = Instantiate(_receiptTemplateUI, _container);
                waitingReceipUI.SetReceiptName(receipt.Name);
                waitingReceipUI.SetIngredientIcons(receipt.KitchenObjsSO);
                waitingReceipUI.Show();
            }
        }

        private void ClearPreviousVisual()
        {
            foreach (Transform child in _container)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
