using UnityEngine;

public sealed class CuttingCounter : BaseCounter
{
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
            if (!playerController.HasKitchenObj())
            {
                _curCuttingProcess = 0;
                Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(0, gameObject.GetInstanceID());

                GetKitchenObj().SetCurKitchenObjParent(playerController);
            }
        }
        else
        {
            if (playerController.HasKitchenObj() && HasReceiptWithInput(playerController.GetKitchenObj().GetKitchenObjectSO()))
            {
                playerController.GetKitchenObj().SetCurKitchenObjParent(this);
                UpdateCounterProgress(0);
            }
        }
    }

    public override void OnCuttingInteract(PlayerController playerController)
    {
        if (HasKitchenObj() && HasReceiptWithInput(GetKitchenObj().GetKitchenObjectSO()))
        {
            TriggerCutAnim();
            UpdateCounterProgress(++_curCuttingProcess);
        }
    }

    private void UpdateCounterProgress(int newCuttingProgress)
    {
        _curCuttingProcess = newCuttingProgress;
        CuttingReceiptSO outputCuttingReceiptSO = GetCuttingReceiptSOWithInput(GetKitchenObj().GetKitchenObjectSO());
        float progressNormalized = (float)_curCuttingProcess / outputCuttingReceiptSO.CuttingProcessMax;

        Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(progressNormalized, gameObject.GetInstanceID());

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
