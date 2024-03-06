namespace KitchenChaos
{
    public sealed class TrashCounter : BaseCounter
    {
        public override void OnMainInteract(PlayerInteraction player)
        {
            if (!player.HasKitchenObj)
            {
                return;
            }

            player.KitchenObj.DestroySelf();
            _eventMgr!.InteractWithTrashCounter?.Invoke();
        }
    }
}
