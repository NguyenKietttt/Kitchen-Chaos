using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class DeliveryCounter : BaseCounter
    {
        private DeliveryManager _deliveryMgr;

        public override void OnMainInteract(PlayerInteraction player)
        {
            if (player.HasKitchenObj && player.KitchenObj.TryGetPlate(out PlateKitchenObject plateKitchenObj))
            {
                _deliveryMgr.DeliveryReceipt(plateKitchenObj);
                player.KitchenObj.DestroySelf();
            }
        }

        protected override void RegisterServices()
        {
            base.RegisterServices();
            _deliveryMgr = ServiceLocator.Instance.Get<DeliveryManager>();
        }

        protected override void DeregisterServices()
        {
            base.DeregisterServices();
            _deliveryMgr = null;
        }
    }
}
