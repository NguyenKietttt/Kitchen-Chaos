using UnityEngine;

public sealed class ContainerCounter : BaseCounter
{
    [Header("SO")]
    [SerializeField] private KitchenObjectSO _kitchenObjSO;

    [Header("Child Internal Ref")]
    [SerializeField] private Animator _animator;

    private readonly int _openCloseKeyHash = Animator.StringToHash("OpenClose");

    public override void OnInteract(PlayerController playerController)
    {
        if (playerController.HasKitchenObj())
        {
            return;
        }

        TriggerAnimationClid();
        KitchenObject.SpawnKitchenObj(_kitchenObjSO, playerController);
    }

    private void TriggerAnimationClid()
    {
        _animator.SetTrigger(_openCloseKeyHash);
    }
}
