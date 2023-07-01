public sealed class ContainerCounter : BaseCounter
{
    public override void OnInteract(PlayerController playerController)
    {
        if (HasKitchenObj())
        {
            return;
        }

        KitchenObject kitchenObj = Instantiate(_kitchenObjSO.Prefab).GetComponent<KitchenObject>();
        kitchenObj.SetCurKitchenObjParent(playerController);

        Bootstrap.Instance.EventMgr.OnPlayerGrabObj?.Invoke();
    }
}
