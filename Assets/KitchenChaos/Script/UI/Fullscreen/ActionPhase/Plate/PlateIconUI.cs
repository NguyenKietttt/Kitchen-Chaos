using System.Collections.Generic;
using KitchenChaos.Utils;
using UnityEngine;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class PlateIconUI : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private PlateIconUICfg? _config;

        [Header("Internal Ref")]
        [SerializeField] private PlateKitchenObject? _plateKitchenObj;

        private EventManager? _eventMgr;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void Awake()
        {
            RegisterServices();
        }

        private void Start()
        {
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
            DeregisterServices();
        }

        private void UpdateVisual(int senderID, KitchenObjectSO kitchenObjSO)
        {
            if (senderID != _plateKitchenObj!.GetInstanceID())
            {
                return;
            }

            ClearPreviousVisual();

            IReadOnlyCollection<KitchenObjectSO> kitchenObjHashSet = _plateKitchenObj.KitchenObjHashSet;
            foreach (KitchenObjectSO kitchenObjectSO in kitchenObjHashSet)
            {
                PlateIconSingleUI plateIconSingleUI = Instantiate(_config!.PlateIconSingleUI, transform);
                plateIconSingleUI.SetIcon(kitchenObjectSO);
            }
        }

        private void ClearPreviousVisual()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void CheckNullEditorReferences()
        {
            if (_config == null || _plateKitchenObj == null)
            {
                CustomLog.LogError(this, "missing internal references in editor!");
            }
        }

        private void RegisterServices()
        {
            _eventMgr = ServiceLocator.Instance.Get<EventManager>();
        }

        private void DeregisterServices()
        {
            _eventMgr = null;
        }

        private void SubscribeEvents()
        {
            _eventMgr!.AddIngredientSuccess += UpdateVisual;
        }

        private void UnsubscribeEvents()
        {
            _eventMgr!.AddIngredientSuccess -= UpdateVisual;
        }
    }
}
