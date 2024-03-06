using KitchenChaos.Utils;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class ContainerCounter : BaseCounter
    {
        [Header("Child Config")]
        [SerializeField] private KitchenObjectSO? _kitchenObjSO;

        [Header("Child Internal Ref")]
        [SerializeField] private Animator? _animator;

        private readonly int _lidAnimKeyHash = Animator.StringToHash("OpenClose");

        public override void OnMainInteract(PlayerInteraction player)
        {
            if (player.HasKitchenObj)
            {
                return;
            }

            TriggerLidAnim();
            KitchenObject.SpawnKitchenObj(_kitchenObjSO!, player);
        }

        protected override void CheckNullEditorReferences()
        {
            base.CheckNullEditorReferences();

            if (_kitchenObjSO == null || _animator == null)
            {
                CustomLog.LogError(this, "missing internal references in editor!");
            }
        }

        private void TriggerLidAnim()
        {
            _animator!.SetTrigger(_lidAnimKeyHash);
        }
    }
}
