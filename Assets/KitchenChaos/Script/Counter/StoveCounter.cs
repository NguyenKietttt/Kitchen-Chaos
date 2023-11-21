using UnityEngine;

public sealed class StoveCounter : BaseCounter
{
    public enum State { Idle, Frying, Fried, Burned }

    public bool IsFried => _curState is State.Fried;

    [Header("Child Asset Ref")]
    [SerializeField] private FryingReceiptSO[] _listFryingReceiptSO;
    [SerializeField] private BurningReceiptSO[] _listBurningReceiptSO;

    private FryingReceiptSO _fryingReceiptSO;
    private BurningReceiptSO _burningReceiptSO;
    private State _curState;
    private float _fryingTimer;
    private float _burningTimer;

    protected override void Start()
    {
        base.Start();
        _curState = State.Idle;
    }

    private void Update()
    {
        if (!HasKitchenObj())
        {
            return;
        }

        float progressNormalized;

        switch (_curState)
        {
            case State.Frying:
                _fryingTimer += Time.deltaTime;

                progressNormalized = _fryingTimer / _fryingReceiptSO.FryingTimeMax;
                Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(progressNormalized, gameObject.GetInstanceID());

                if (_fryingTimer >= _fryingReceiptSO.FryingTimeMax)
                {
                    GetKitchenObj().DestroySelf();
                    KitchenObject.SpawnKitchenObj(_fryingReceiptSO.Output, this);

                    _burningTimer = 0;
                    _curState = State.Fried;
                    _burningReceiptSO = GetBurningReceiptSOWithInput(GetKitchenObj().GetKitchenObjectSO());

                    Bootstrap.Instance.EventMgr.ChangeStoveCounterState?.Invoke(_curState, gameObject.GetInstanceID());
                }
                break;
            case State.Fried:
                _burningTimer += Time.deltaTime;

                progressNormalized = _burningTimer / _burningReceiptSO.BurningTimeMax;
                Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(progressNormalized, gameObject.GetInstanceID());

                if (_burningTimer >= _burningReceiptSO.BurningTimeMax)
                {
                    GetKitchenObj().DestroySelf();
                    KitchenObject.SpawnKitchenObj(_burningReceiptSO.Output, this);

                    _curState = State.Burned;

                    Bootstrap.Instance.EventMgr.ChangeStoveCounterState?.Invoke(_curState, gameObject.GetInstanceID());
                    Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(0, gameObject.GetInstanceID());
                }
                break;
        }
    }

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

                        _curState = State.Idle;

                        Bootstrap.Instance.EventMgr.ChangeStoveCounterState?.Invoke(_curState, gameObject.GetInstanceID());
                        Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(0, gameObject.GetInstanceID());
                    }
                }
            }
            else
            {
                GetKitchenObj().SetCurKitchenObjParent(playerController);

                _curState = State.Idle;

                Bootstrap.Instance.EventMgr.ChangeStoveCounterState?.Invoke(_curState, gameObject.GetInstanceID());
                Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(0, gameObject.GetInstanceID());
            }
        }
        else
        {
            if (playerController.HasKitchenObj() && HasReceiptWithInput(playerController.GetKitchenObj().GetKitchenObjectSO()))
            {
                playerController.GetKitchenObj().SetCurKitchenObjParent(this);
                _fryingReceiptSO = GetFryingReceiptSOWithInput(GetKitchenObj().GetKitchenObjectSO());

                _fryingTimer = 0;
                _curState = State.Frying;

                Bootstrap.Instance.EventMgr.ChangeStoveCounterState?.Invoke(_curState, gameObject.GetInstanceID());

                float progressNormalized = _fryingTimer / _fryingReceiptSO.FryingTimeMax;
                Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(progressNormalized, gameObject.GetInstanceID());
            }
        }
    }

    private bool HasReceiptWithInput(KitchenObjectSO inputKitchenObjSO)
    {
        return GetFryingReceiptSOWithInput(inputKitchenObjSO) != null;
    }

    private FryingReceiptSO GetFryingReceiptSOWithInput(KitchenObjectSO inputKitchenObjSO)
    {
        foreach (FryingReceiptSO fryingReceiptSO in _listFryingReceiptSO)
        {
            if (fryingReceiptSO.Input == inputKitchenObjSO)
            {
                return fryingReceiptSO;
            }
        }

        return null;
    }

    private BurningReceiptSO GetBurningReceiptSOWithInput(KitchenObjectSO inputKitchenObjSO)
    {
        foreach (BurningReceiptSO burningReceiptSO in _listBurningReceiptSO)
        {
            if (burningReceiptSO.Input == inputKitchenObjSO)
            {
                return burningReceiptSO;
            }
        }

        return null;
    }
}
