namespace KitchenChaos
{
    public sealed class DeliveryCounter : BaseCounter
    {
        public override void OnInteract(PlayerInteraction player)
        {
            if (player.HasKitchenObj)
            {
                if (player.KitchenObj.TryGetPlate(out PlateKitchenObject plateKitchenObj))
                {
                    Bootstrap.Instance.DeliveryMgr.DeliveryReceipt(plateKitchenObj);
                    player.KitchenObj.DestroySelf();
                }
            }
        }
    }
}
