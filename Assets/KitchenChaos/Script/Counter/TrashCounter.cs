using System;

namespace KitchenChaos
{
    public sealed class TrashCounter : BaseCounter
    {
        public override void OnInteract(PlayerController playerController)
        {
            if (playerController.HasKitchenObj())
            {
                playerController.GetKitchenObj().DestroySelf();
                Bootstrap.Instance.EventMgr.InteractWithTrashCounter?.Invoke();
            }
        }
    }
}
