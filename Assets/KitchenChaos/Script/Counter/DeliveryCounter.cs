using UnityEngine;

public sealed class DeliveryCounter : BaseCounter
{
    public override void OnInteract(PlayerController playerController)
    {
        if (playerController.HasKitchenObj())
        {
            if (playerController.GetKitchenObj().TryGetPlate(out PlateKitchenObject plateKitchenObj))
            {
                Bootstrap.Instance.DeliveryMgr.DeliveryReceipt(plateKitchenObj);
                playerController.GetKitchenObj().DestroySelf();
            }
        }
    }
}
