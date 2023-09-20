using System;

public sealed class TrashCounter : BaseCounter
{
    public static event Action ObjectTrashed;

    public override void OnInteract(PlayerController playerController)
    {
        if (playerController.HasKitchenObj())
        {
            playerController.GetKitchenObj().DestroySelf();
            ObjectTrashed?.Invoke();
        }
    }
}
