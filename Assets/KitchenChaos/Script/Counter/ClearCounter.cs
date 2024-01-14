namespace KitchenChaos
{
    public sealed class ClearCounter : BaseCounter
    {
        public override void OnInteract(PlayerController playerController)
        {
            if (HasKitchenObj())
            {
                if (playerController.HasKitchenObj())
                {
                    if (playerController.GetKitchenObj().TryGetPlate(out PlateKitchenObject plateKitchenObj))
                    {
                        KitchenObject kitchenObj = GetKitchenObj();
                        if (plateKitchenObj.TryAddIngredient(kitchenObj.GetKitchenObjectSO()))
                        {
                            kitchenObj.DestroySelf();
                        }
                    }
                    else
                    {
                        if (GetKitchenObj().TryGetPlate(out plateKitchenObj))
                        {
                            if (plateKitchenObj.TryAddIngredient(playerController.GetKitchenObj().GetKitchenObjectSO()))
                            {
                                playerController.GetKitchenObj().DestroySelf();
                            }
                        }
                    }
                }
                else
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
    }
}
