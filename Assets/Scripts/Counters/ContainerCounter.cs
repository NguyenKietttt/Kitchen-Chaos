using UnityEngine;

public sealed class ContainerCounter : BaseCounter
{
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

        KitchenObject kitchenObj = Instantiate(_kitchenObjSO.Prefab).GetComponent<KitchenObject>();
        kitchenObj.SetCurKitchenObjParent(playerController);
    }

    private void TriggerAnimationClid()
    {
        _animator.SetTrigger(_openCloseKeyHash);
    }
}
