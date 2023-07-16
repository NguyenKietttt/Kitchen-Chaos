using UnityEngine;

public sealed class CuttingCounter : BaseCounter
{
    [Header("SO")]
    [SerializeField] private CuttingReceiptSO[] _listCuttingReceiptSO;

    public override void OnInteract(PlayerController playerController)
    {
        if (HasKitchenObj())
        {
            if (!playerController.HasKitchenObj())
            {
                GetKitchenObj().SetCurKitchenObjParent(playerController);
            }
        }
        else
        {
            if (playerController.HasKitchenObj() && HasReceiptWithInput(playerController.GetKitchenObj().GetKitchenObjectSO()))
            {
                playerController.GetKitchenObj().SetCurKitchenObjParent(this);
            }
        }
    }

    public override void OnCuttingInteract(PlayerController playerController)
    {
        if (HasKitchenObj() && HasReceiptWithInput(GetKitchenObj().GetKitchenObjectSO()))
        {
            KitchenObjectSO outputKitchenObjSO = GetOutputForInput(GetKitchenObj().GetKitchenObjectSO());

            GetKitchenObj().DestroySelf();
            KitchenObject.SpawnKitchenObj(outputKitchenObjSO, this);
        }
    }

    private bool HasReceiptWithInput(KitchenObjectSO inputKitchenObjSO)
    {
        foreach (CuttingReceiptSO cuttingReceiptSO in _listCuttingReceiptSO)
        {
            if (cuttingReceiptSO.Input == inputKitchenObjSO)
            {
                return true;
            }
        }

        return false;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjSO)
    {
        foreach (CuttingReceiptSO cuttingReceiptSO in _listCuttingReceiptSO)
        {
            if (cuttingReceiptSO.Input == inputKitchenObjSO)
            {
                return cuttingReceiptSO.Output;
            }
        }

        return null;
    }
}
