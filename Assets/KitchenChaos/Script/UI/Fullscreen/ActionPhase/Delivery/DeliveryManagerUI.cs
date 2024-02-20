using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class DeliveryManagerUI : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private DeliveryManagerUICfg _config;

        [Header("Internal Ref")]
        [SerializeField] private Transform _container;

        private void Awake()
        {
            _config.ReceiptTemplateUI.Hide();
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
                DeliveryManagerSingleUI waitingReceiptUI = Instantiate(_config.ReceiptTemplateUI, _container);
                waitingReceiptUI.SetReceiptName(receipt.Name);
                waitingReceiptUI.SetIngredientIcons(receipt.KitchenObjsSO);
                waitingReceiptUI.Show();
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
