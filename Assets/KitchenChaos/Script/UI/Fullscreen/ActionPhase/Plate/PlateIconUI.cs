using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlateIconUI : MonoBehaviour
    {
        [Header("Asset Ref")]
        [SerializeField] private PlateIconSingleUI _plateIconSingleUI;

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

            HashSet<KitchenObjectSO> listKitchenObjSO = _plateKitchenObj.GetListKitchenObjectSO();
            foreach (KitchenObjectSO kitchenObjectSO in listKitchenObjSO)
            {
                PlateIconSingleUI plateIconSingleUI = Instantiate(_plateIconSingleUI, transform);
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
