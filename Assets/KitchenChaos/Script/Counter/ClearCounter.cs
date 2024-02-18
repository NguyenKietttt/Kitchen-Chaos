namespace KitchenChaos
{
    public sealed class ClearCounter : BaseCounter
    {
        public override void OnInteract(PlayerInteraction player)
        {
            if (HasKitchenObj)
            {
                if (player.HasKitchenObj)
                {
                    if (player.KitchenObj.TryGetPlate(out PlateKitchenObject plateKitchenObj))
                    {
                        KitchenObject kitchenObj = KitchenObj;
                        if (plateKitchenObj.TryAddIngredient(kitchenObj.KitchenObjectSO))
                        {
                            kitchenObj.DestroySelf();
                        }
                    }
                    else
                    {
                        if (KitchenObj.TryGetPlate(out plateKitchenObj))
                        {
                            if (plateKitchenObj.TryAddIngredient(player.KitchenObj.KitchenObjectSO))
                            {
                                player.KitchenObj.DestroySelf();
                            }
                        }
                    }
                }
                else
                {
                    KitchenObj.SetCurKitchenObjParent(player);
                }
            }
            else
            {
                if (player.HasKitchenObj)
                {
                    player.KitchenObj.SetCurKitchenObjParent(this);
                }
            }
        }
    }
}
