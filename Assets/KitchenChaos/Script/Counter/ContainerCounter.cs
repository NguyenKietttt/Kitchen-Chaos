using UnityEngine;

namespace KitchenChaos
{
    public sealed class ContainerCounter : BaseCounter
    {
        [Header("Child Config")]
        [SerializeField] private KitchenObjectSO _kitchenObjSO;

        [Header("Child Internal Ref")]
        [SerializeField] private Animator _animator;

        private readonly int _clidAnimKeyHash = Animator.StringToHash("OpenClose");

        public override void OnInteract(PlayerController playerController)
        {
            if (playerController.HasKitchenObj)
            {
                return;
            }

            TriggerClidAnim();
            KitchenObject.SpawnKitchenObj(_kitchenObjSO, playerController);
        }

        private void TriggerClidAnim()
        {
            _animator.SetTrigger(_clidAnimKeyHash);
        }
    }
}
