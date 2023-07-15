using UnityEngine;

public sealed class CuttingCounter : BaseCounter
{
    [Header("SO")]
    [SerializeField] private KitchenObjectSO _cutKitchenObjSO;

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
            if (playerController.HasKitchenObj())
            {
                playerController.GetKitchenObj().SetCurKitchenObjParent(this);
            }
        }
    }

    public override void OnCuttingInteract(PlayerController playerController)
    {
        if (HasKitchenObj())
        {
            GetKitchenObj().DestroySelf();
            KitchenObject.SpawnKitchenObj(_cutKitchenObjSO, this);
        }
    }
}
