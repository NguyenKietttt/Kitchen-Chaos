using UnityEngine;

namespace KitchenChaos
{
    public sealed class CuttingCounter : BaseCounter, ISecondaryInteractHandler
    {
        private const int MIN_PROGRESS = 0;

        [Header("Child Config")]
        [SerializeField] private CuttingReceiptSO[] _cuttingReceipts;

        [Header("Child Internal Ref")]
        [SerializeField] private Animator _animator;

        private readonly int _cutAnimKeyHash = Animator.StringToHash("Cut");

        private int _curCuttingProcess;

        public override void OnMainInteract(PlayerInteraction player)
        {
            if (HasKitchenObj)
            {
                if (player.HasKitchenObj)
                {
                    if (player.KitchenObj.TryGetPlate(out PlateKitchenObject plateKitchenObj))
                    {
                        KitchenObject kitchenObj = KitchenObj;
                        if (plateKitchenObj.TryAddIngredient(kitchenObj.KitchenObjectSO))
                        {
                            kitchenObj.DestroySelf();
                        }
                    }
                }
                else
                {
                    _curCuttingProcess = MIN_PROGRESS;
                    _eventMgr.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), MIN_PROGRESS);

                    KitchenObj.SetCurKitchenObjParent(player);
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
            if (HasKitchenObj && HasReceiptWithInput(KitchenObj.KitchenObjectSO))
            {
                TriggerCutAnim();
                UpdateCounterProgress(++_curCuttingProcess);

                _eventMgr.InteractWithCutCounter?.Invoke();
            }
        }

        private void UpdateCounterProgress(int newCuttingProgress)
        {
            _curCuttingProcess = newCuttingProgress;
            CuttingReceiptSO outputCuttingReceiptSO = GetCuttingReceiptSOWithInput(KitchenObj.KitchenObjectSO);
            float progressNormalized = (float)_curCuttingProcess / outputCuttingReceiptSO.CuttingProcessMax;

            _eventMgr.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), progressNormalized);

            SpawnOutputCuttingKitchenObj(outputCuttingReceiptSO);
        }

        private void SpawnOutputCuttingKitchenObj(CuttingReceiptSO outputCuttingReceiptSO)
        {
            if (_curCuttingProcess >= outputCuttingReceiptSO.CuttingProcessMax)
            {
                KitchenObj.DestroySelf();
                KitchenObject.SpawnKitchenObj(outputCuttingReceiptSO.Output, this);
            }
        }

        private bool HasReceiptWithInput(KitchenObjectSO inputKitchenObjSO)
        {
            return GetCuttingReceiptSOWithInput(inputKitchenObjSO) != null;
        }

        private CuttingReceiptSO GetCuttingReceiptSOWithInput(KitchenObjectSO inputKitchenObjSO)
        {
            foreach (CuttingReceiptSO cuttingReceipt in _cuttingReceipts)
            {
                if (cuttingReceipt.Input == inputKitchenObjSO)
                {
                    return cuttingReceipt;
                }
            }

            return null;
        }

        private void TriggerCutAnim()
        {
            _animator.SetTrigger(_cutAnimKeyHash);
        }
    }
}
