namespace KitchenChaos
{
    public sealed class TrashCounter : BaseCounter
    {
        public override void OnInteract(PlayerInteraction player)
        {
            if (player.HasKitchenObj)
            {
                player.KitchenObj.DestroySelf();
                Bootstrap.Instance.EventMgr.InteractWithTrashCounter?.Invoke();
            }
        }
    }
}
