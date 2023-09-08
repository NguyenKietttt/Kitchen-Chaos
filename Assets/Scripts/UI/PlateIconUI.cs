using System.Collections.Generic;
using UnityEngine;

public sealed class PlateIconUI : MonoBehaviour
{
    [Header("External Ref")]
    [SerializeField] private PlateKitchenObject _plateKitchenObj;

    [Header("Internal Ref")]
    [SerializeField] private PlateIconSingleUI _plateIconSingleUI;

    private void Start()
    {
        Bootstrap.Instance.EventMgr.AddIngredientSuccess += UpdateVisual;
    }

    private void OnDestroy()
    {
        Bootstrap.Instance.EventMgr.AddIngredientSuccess -= UpdateVisual;
    }

    private void UpdateVisual(int plateID, KitchenObjectSO kitchenObjSO)
    {
        if (plateID != _plateKitchenObj.GetInstanceID())
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
