public sealed class ClearCounter : BaseCounter
{
    public override void OnInteract(PlayerController playerController)
    {
        if (HasKitchenObj())
        {
            if (!playerController.HasKitchenObj())
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