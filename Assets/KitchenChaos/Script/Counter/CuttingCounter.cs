using System;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class CuttingCounter : BaseCounter
    {
        private const int MIN_PROGRESS = 0;

        [Header("Child Internal Ref")]
        [SerializeField] private Animator _animator;

        [Header("SO")]
        [SerializeField] private CuttingReceiptSO[] _listCuttingReceiptSO;

        private readonly int _cutAnimKeyHash = Animator.StringToHash("Cut");

        private int _curCuttingProcess;

        public override void OnInteract(PlayerController playerController)
        {
            if (HasKitchenObj())
            {
                if (playerController.HasKitchenObj())
                {
                    if (playerController.GetKitchenObj().TryGetPlate(out PlateKitchenObject plateKitchenObj))
                    {
                        KitchenObject kitchenObj = GetKitchenObj();
                        if (plateKitchenObj.TryAddIngredient(kitchenObj.GetKitchenObjectSO()))
                        {
                            kitchenObj.DestroySelf();
                        }
                    }
                }
                else
                {
                    _curCuttingProcess = MIN_PROGRESS;
                    Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(MIN_PROGRESS, gameObject.GetInstanceID());

                    GetKitchenObj().SetCurKitchenObjParent(playerController);
                }
            }
            else
            {
                if (playerController.HasKitchenObj() && HasReceiptWithInput(playerController.GetKitchenObj().GetKitchenObjectSO()))
                {
                    playerController.GetKitchenObj().SetCurKitchenObjParent(this);
                    UpdateCounterProgress(MIN_PROGRESS);
                }
            }
        }

        public override void OnCuttingInteract(PlayerController playerController)
        {
            if (HasKitchenObj() && HasReceiptWithInput(GetKitchenObj().GetKitchenObjectSO()))
            {
                TriggerCutAnim();
                UpdateCounterProgress(++_curCuttingProcess);

                Bootstrap.Instance.EventMgr.InteractWithCutCounter?.Invoke();
            }
        }

        private void UpdateCounterProgress(int newCuttingProgress)
        {
            _curCuttingProcess = newCuttingProgress;
            CuttingReceiptSO outputCuttingReceiptSO = GetCuttingReceiptSOWithInput(GetKitchenObj().GetKitchenObjectSO());
            float progressNormalized = (float)_curCuttingProcess / outputCuttingReceiptSO.CuttingProcessMax;

            Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), progressNormalized);

            SpawnOutputCuttingKitchenObj(outputCuttingReceiptSO);
        }

        private void SpawnOutputCuttingKitchenObj(CuttingReceiptSO outputCuttingReceiptSO)
        {
            if (_curCuttingProcess >= outputCuttingReceiptSO.CuttingProcessMax)
            {
                GetKitchenObj().DestroySelf();
                KitchenObject.SpawnKitchenObj(outputCuttingReceiptSO.Output, this);
            }
        }

        private bool HasReceiptWithInput(KitchenObjectSO inputKitchenObjSO)
        {
            return GetCuttingReceiptSOWithInput(inputKitchenObjSO) != null;
        }

        private CuttingReceiptSO GetCuttingReceiptSOWithInput(KitchenObjectSO inputKitchenObjSO)
        {
            foreach (CuttingReceiptSO cuttingReceiptSO in _listCuttingReceiptSO)
            {
                if (cuttingReceiptSO.Input == inputKitchenObjSO)
                {
                    return cuttingReceiptSO;
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
