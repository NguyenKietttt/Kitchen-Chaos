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

            IEnumerable<ReceiptSO> listWaitingReceiptSO = Bootstrap.Instance.DeliveryMgr.ListWaitingReceiptSO;
            foreach (ReceiptSO receiptSO in listWaitingReceiptSO)
            {
                DeliveryManagerSingleUI waitingReceipUI = Instantiate(_receiptTemplateUI, _container);
                waitingReceipUI.SetReceiptName(receiptSO.ReceiptName);
                waitingReceipUI.SetIngredientIcons(receiptSO.ListKitchenObjSO);
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
