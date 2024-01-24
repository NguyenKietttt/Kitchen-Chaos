namespace KitchenChaos
{
    public sealed class ClearCounter : BaseCounter
    {
        public override void OnInteract(PlayerController playerController)
        {
            if (HasKitchenObj)
            {
                if (playerController.HasKitchenObj)
                {
                    if (playerController.KitchenObj.TryGetPlate(out PlateKitchenObject plateKitchenObj))
                    {
                        KitchenObject kitchenObj = KitchenObj;
                        if (plateKitchenObj.TryAddIngredient(kitchenObj.GetKitchenObjectSO()))
                        {
                            kitchenObj.DestroySelf();
                        }
                    }
                    else
                    {
                        if (KitchenObj.TryGetPlate(out plateKitchenObj))
                        {
                            if (plateKitchenObj.TryAddIngredient(playerController.KitchenObj.GetKitchenObjectSO()))
                            {
                                playerController.KitchenObj.DestroySelf();
                            }
                        }
                    }
                }
                else
                {
                    KitchenObj.SetCurKitchenObjParent(playerController);
                }
            }
            else
            {
                if (playerController.HasKitchenObj)
                {
                    playerController.KitchenObj.SetCurKitchenObjParent(this);
                }
            }
        }
    }
}
