using System.Linq;
using KitchenChaos.Utils;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class CuttingCounter : BaseCounter, ISecondaryInteractHandler
    {
        private const int MIN_PROGRESS = 0;

        [Header("Child Config")]
        [SerializeField] private CuttingReceiptSO[]? _cuttingReceipts;

        [Header("Child Internal Ref")]
        [SerializeField] private Animator? _animator;

        private readonly int _cutAnimKeyHash = Animator.StringToHash("Cut");

        private int _curCuttingProcess;

        public override void OnMainInteract(PlayerInteraction player)
        {
            if (HasKitchenObj)
            {
                if (player.HasKitchenObj)
                {
                    if (player.KitchenObj.TryGetPlate(out PlateKitchenObject? plateKitchenObj))
                    {
                        KitchenObject? kitchenObj = _curKitchenObj;
                        if (plateKitchenObj!.TryAddIngredient(kitchenObj!.KitchenObjectSO))
                        {
                            kitchenObj.DestroySelf();
                        }
                    }
                }
                else
                {
                    _curCuttingProcess = MIN_PROGRESS;
                    _eventMgr!.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), MIN_PROGRESS);

                    _curKitchenObj!.SetCurKitchenObjParent(player);
                }
            }
            else
            {
                if (player.HasKitchenObj && HasReceiptWithInput(player.KitchenObj.KitchenObjectSO))
                {
                    player.KitchenObj.SetCurKitchenObjParent(this);
                    UpdateCounterProgress(MIN_PROGRESS);
                }
            }
        }

        public void OnSecondaryInteract()
        {
            if (HasKitchenObj && HasReceiptWithInput(_curKitchenObj!.KitchenObjectSO))
            {
                TriggerCutAnim();
                UpdateCounterProgress(++_curCuttingProcess);

                _eventMgr!.InteractWithCutCounter?.Invoke();
            }
        }

        protected override void CheckNullEditorReferences()
        {
            base.CheckNullEditorReferences();

            if (_animator == null || _cuttingReceipts?.Length <= 0)
            {
                CustomLog.LogError(this, "missing references in editor!!!");
            }
        }

        private void UpdateCounterProgress(int newCuttingProgress)
        {
            if (_curKitchenObj == null)
            {
                CustomLog.LogError(this, nameof(_curKitchenObj), "Somehow is null!!!");
                return;
            }

            CuttingReceiptSO? outputCuttingReceiptSO = GetCuttingReceiptSOWithInput(_curKitchenObj.KitchenObjectSO);

            if (outputCuttingReceiptSO == null)
            {
                CustomLog.LogError(this, nameof(outputCuttingReceiptSO), "There is no match receipt!!!");
                return;
            }

            _curCuttingProcess = newCuttingProgress;
            float progressNormalized = (float)_curCuttingProcess / outputCuttingReceiptSO.CuttingProcessMax;
            _eventMgr!.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), progressNormalized);
            SpawnOutputCuttingKitchenObj(outputCuttingReceiptSO);
        }

        private void SpawnOutputCuttingKitchenObj(CuttingReceiptSO outputCuttingReceiptSO)
        {
            if (_curCuttingProcess >= outputCuttingReceiptSO.CuttingProcessMax && HasKitchenObj)
            {
                _curKitchenObj!.DestroySelf();
                KitchenObject.SpawnKitchenObj(outputCuttingReceiptSO.Output, this);
            }
        }

        private bool HasReceiptWithInput(KitchenObjectSO inputKitchenObjSO)
        {
            return GetCuttingReceiptSOWithInput(inputKitchenObjSO) != null;
        }

        private CuttingReceiptSO? GetCuttingReceiptSOWithInput(KitchenObjectSO inputKitchenObjSO)
        {
            return _cuttingReceipts!.FirstOrDefault(p => p.Input == inputKitchenObjSO);
        }

        private void TriggerCutAnim()
        {
            _animator!.SetTrigger(_cutAnimKeyHash);
        }
    }
}
