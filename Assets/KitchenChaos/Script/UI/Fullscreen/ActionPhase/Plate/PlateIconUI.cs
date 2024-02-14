using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlateIconUI : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private PlateIconUICfg _config;

        [Header("Internal Ref")]
        [SerializeField] private PlateKitchenObject _plateKitchenObj;

        private void Start()
        {
            Bootstrap.Instance.EventMgr.AddIngredientSuccess += UpdateVisual;
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.AddIngredientSuccess -= UpdateVisual;
        }

        private void UpdateVisual(int senderID, KitchenObjectSO kitchenObjSO)
        {
            if (senderID != _plateKitchenObj.GetInstanceID())
            {
                return;
            }

            ClearPreviousVisual();

            IReadOnlyCollection<KitchenObjectSO> kitchenObjHashSet = _plateKitchenObj.KitchenObjHashSet;
            foreach (KitchenObjectSO kitchenObjectSO in kitchenObjHashSet)
            {
                PlateIconSingleUI plateIconSingleUI = Instantiate(_config.PlateIconSingleUI, transform);
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
    }
}
