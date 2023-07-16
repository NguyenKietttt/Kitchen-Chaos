using UnityEngine;

public sealed class CuttingCounter : BaseCounter
{
    [Header("SO")]
    [SerializeField] private CuttingReceiptSO[] _listCuttingReceiptSO;

    [Header("Child Internal Ref")]
    [SerializeField] private Animator _animator;

    private readonly int _cutKeyHash = Animator.StringToHash("Cut");

    private int _cuttingProcess;

    public override void OnInteract(PlayerController playerController)
    {
        if (HasKitchenObj())
        {
            if (!playerController.HasKitchenObj())
            {
                GetKitchenObj().SetCurKitchenObjParent(playerController);
            }
        }
        else
        {
            if (playerController.HasKitchenObj() && HasReceiptWithInput(playerController.GetKitchenObj().GetKitchenObjectSO()))
            {
                playerController.GetKitchenObj().SetCurKitchenObjParent(this);
                _cuttingProcess = 0;

                CuttingReceiptSO outputCuttingReceiptSO = GetCuttingReceiptSOWithInput(GetKitchenObj().GetKitchenObjectSO());

                float progressNormalized = (float)_cuttingProcess / outputCuttingReceiptSO.CuttingProcessMax;
                Bootstrap.Instance.EventMgr.OnProgressChanged?.Invoke(progressNormalized);
            }
        }
    }

    public override void OnCuttingInteract(PlayerController playerController)
    {
        if (HasKitchenObj() && HasReceiptWithInput(GetKitchenObj().GetKitchenObjectSO()))
        {
            _cuttingProcess++;

            TriggerAnimationCut();
            CuttingReceiptSO outputCuttingReceiptSO = GetCuttingReceiptSOWithInput(GetKitchenObj().GetKitchenObjectSO());

            float progressNormalized = (float)_cuttingProcess / outputCuttingReceiptSO.CuttingProcessMax;
            Bootstrap.Instance.EventMgr.OnProgressChanged?.Invoke(progressNormalized);

            if (_cuttingProcess >= outputCuttingReceiptSO.CuttingProcessMax)
            {
                GetKitchenObj().DestroySelf();
                KitchenObject.SpawnKitchenObj(outputCuttingReceiptSO.Output, this);
            }
        }
    }

    private bool HasReceiptWithInput(KitchenObjectSO inputKitchenObjSO)
    {
        return GetCuttingReceiptSOWithInput(inputKitchenObjSO) != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjSO)
    {
        CuttingReceiptSO cuttingReceiptSO = GetCuttingReceiptSOWithInput(inputKitchenObjSO);

        if (cuttingReceiptSO != null)
        {
            return cuttingReceiptSO.Output;
        }

        return null;
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

    private void TriggerAnimationCut()
    {
        _animator.SetTrigger(_cutKeyHash);
    }
}
