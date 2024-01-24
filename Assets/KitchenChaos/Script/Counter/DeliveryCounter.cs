namespace KitchenChaos
{
    public sealed class DeliveryCounter : BaseCounter
    {
        public override void OnInteract(PlayerController playerController)
        {
            if (playerController.HasKitchenObj)
            {
                if (playerController.KitchenObj.TryGetPlate(out PlateKitchenObject plateKitchenObj))
                {
                    Bootstrap.Instance.DeliveryMgr.DeliveryReceipt(plateKitchenObj);
                    playerController.KitchenObj.DestroySelf();
                }
            }
        }
    }
}
