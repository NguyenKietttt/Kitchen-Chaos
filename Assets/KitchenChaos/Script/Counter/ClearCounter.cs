namespace KitchenChaos
{
    public sealed class ClearCounter : BaseCounter
    {
        public override void OnMainInteract(PlayerInteraction player)
        {
            if (!HasKitchenObj && !player.HasKitchenObj)
            {
                return;
            }

            if (!HasKitchenObj && player.HasKitchenObj)
            {
                player.KitchenObj.SetCurKitchenObjParent(this);
                return;
            }

            if (HasKitchenObj && !player.HasKitchenObj)
            {
                KitchenObj.SetCurKitchenObjParent(player);
                return;
            }

            if (player.KitchenObj.TryGetPlate(out PlateKitchenObject plateKitchenObj))
            {
                if (plateKitchenObj.TryAddIngredient(KitchenObj.KitchenObjectSO))
                {
                    KitchenObj.DestroySelf();
                }
            }
            else
            {
                if (KitchenObj.TryGetPlate(out plateKitchenObj) && plateKitchenObj.TryAddIngredient(player.KitchenObj.KitchenObjectSO))
                {
                    player.KitchenObj.DestroySelf();
                }
            }
        }
    }
}
