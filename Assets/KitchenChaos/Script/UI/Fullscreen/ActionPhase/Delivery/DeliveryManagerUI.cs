using System.Collections.Generic;
using KitchenChaos.Utils;
using UnityEngine;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class DeliveryManagerUI : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private DeliveryManagerUICfg? _config;

        [Header("Internal Ref")]
        [SerializeField] private Transform? _container;

        private EventManager? _eventMgr;
        private DeliveryManager? _deliveryMgr;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void Awake()
        {
            RegisterServices();
            _config!.ReceiptTemplateUI.Hide();
        }

        private void Start()
        {
            SubscribeEvents();
            UpdateVisual();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
            DeregisterServices();
        }

        public void UpdateVisual()
        {
            ClearPreviousVisual();

            IEnumerable<DishReceiptSO> waitingDishReceipts = _deliveryMgr!.WaitingReceiptsSO;
            foreach (DishReceiptSO receipt in waitingDishReceipts)
            {
                DeliveryManagerSingleUI waitingReceiptUI = Instantiate(_config!.ReceiptTemplateUI, _container);
                waitingReceiptUI.SetReceiptName(receipt.Name);
                waitingReceiptUI.SetIngredientIcons(receipt.KitchenObjsSO);
                waitingReceiptUI.Show();
            }
        }

        private void ClearPreviousVisual()
        {
            foreach (Transform child in _container!)
            {
                Destroy(child.gameObject);
            }
        }

        private void CheckNullEditorReferences()
        {
            if (_config == null || _container == null)
            {
                CustomLog.LogError(this, "missing internal references in editor!");
            }
        }

        private void RegisterServices()
        {
            _eventMgr = ServiceLocator.Instance.Get<EventManager>();
            _deliveryMgr = ServiceLocator.Instance.Get<DeliveryManager>();
        }

        private void DeregisterServices()
        {
            _eventMgr = null;
            _deliveryMgr = null;
        }

        private void SubscribeEvents()
        {
            _eventMgr!.SpawnReceipt += UpdateVisual;
            _eventMgr!.CompleteReceipt += UpdateVisual;
        }

        private void UnsubscribeEvents()
        {
            _eventMgr!.SpawnReceipt -= UpdateVisual;
            _eventMgr!.CompleteReceipt -= UpdateVisual;
        }
    }
}
