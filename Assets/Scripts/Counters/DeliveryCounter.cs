using UnityEngine;

public sealed class DeliveryCounter : BaseCounter
{
    public override void OnInteract(PlayerController playerController)
    {
        if (playerController.HasKitchenObj())
        {
            if (playerController.GetKitchenObj().TryGetPlate(out PlateKitchenObject plateKitchenObj))
            {
                playerController.GetKitchenObj().DestroySelf();
            }
        }
    }
}
