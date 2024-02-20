namespace KitchenChaos
{
    public sealed class DeliveryCounter : BaseCounter
    {
        public override void OnMainInteract(PlayerInteraction player)
        {
            if (player.HasKitchenObj && player.KitchenObj.TryGetPlate(out PlateKitchenObject plateKitchenObj))
            {
                Bootstrap.Instance.DeliveryMgr.DeliveryReceipt(plateKitchenObj);
                player.KitchenObj.DestroySelf();
            }
        }
    }
}
