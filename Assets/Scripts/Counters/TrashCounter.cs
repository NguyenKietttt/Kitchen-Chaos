public sealed class TrashCounter : BaseCounter
{
    public override void OnInteract(PlayerController playerController)
    {
        if (playerController.HasKitchenObj())
        {
            playerController.GetKitchenObj().DestroySelf();
        }
    }
}