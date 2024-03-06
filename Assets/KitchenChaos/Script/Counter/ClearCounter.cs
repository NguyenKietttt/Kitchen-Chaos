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
                _curKitchenObj!.SetCurKitchenObjParent(player);
                return;
            }

            if (player.KitchenObj.TryGetPlate(out PlateKitchenObject? plateKitchenObj))
            {
                if (plateKitchenObj!.TryAddIngredient(_curKitchenObj!.KitchenObjectSO))
                {
                    _curKitchenObj!.DestroySelf();
                }
            }
            else
            {
                if (_curKitchenObj!.TryGetPlate(out plateKitchenObj) && plateKitchenObj!.TryAddIngredient(player.KitchenObj.KitchenObjectSO))
                {
                    player.KitchenObj.DestroySelf();
                }
            }
        }
    }
}
