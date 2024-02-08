using UnityEngine;

namespace KitchenChaos
{
    public sealed class StoveCounter : BaseCounter
    {
        public bool IsFried => _curState is StoveCounterState.Fried;

        [Header("Child Config")]
        [SerializeField] private StoveCounterCfg _config;

        private FryingReceiptSO _fryingReceiptSO;
        private BurningReceiptSO _burningReceiptSO;
        private StoveCounterState _curState;
        private float _fryingTimer;
        private float _burningTimer;

        protected override void Start()
        {
            base.Start();
            _curState = StoveCounterState.Idle;
        }

        private void Update()
        {
            if (!HasKitchenObj)
            {
                return;
            }

            switch (_curState)
            {
                case StoveCounterState.Frying:
                    OnFryingState();
                    break;
                case StoveCounterState.Fried:
                    OnFriedState();
                    break;
            }
        }

        private void OnFryingState()
        {
            _fryingTimer += Time.deltaTime;

            float progressNormalized = _fryingTimer / _fryingReceiptSO.FryingTimeMax;
            Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), progressNormalized);

            if (_fryingTimer >= _fryingReceiptSO.FryingTimeMax)
            {
                KitchenObj.DestroySelf();
                KitchenObject.SpawnKitchenObj(_fryingReceiptSO.Output, this);

                _burningTimer = _config.BurningTimerMin;
                _curState = StoveCounterState.Fried;
                _burningReceiptSO = GetBurningReceiptSOWithInput(KitchenObj.KitchenObjectSO);

                Bootstrap.Instance.EventMgr.ChangeStoveCounterState?.Invoke(gameObject.GetInstanceID(), _curState);
            }
        }

        private void OnFriedState()
        {
            _burningTimer += Time.deltaTime;

            float progressNormalized = _burningTimer / _burningReceiptSO.BurningTimeMax;
            Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), progressNormalized);

            if (_burningTimer >= _burningReceiptSO.BurningTimeMax)
            {
                KitchenObj.DestroySelf();
                KitchenObject.SpawnKitchenObj(_burningReceiptSO.Output, this);

                _curState = StoveCounterState.Burned;

                Bootstrap.Instance.EventMgr.ChangeStoveCounterState?.Invoke(gameObject.GetInstanceID(), _curState);
                Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), _config.ProgressMin);
            }
        }

        public override void OnInteract(PlayerController playerController)
        {
            if (HasKitchenObj)
            {
                if (playerController.HasKitchenObj)
                {
                    if (playerController.KitchenObj.TryGetPlate(out PlateKitchenObject plateKitchenObj))
                    {
                        KitchenObject kitchenObj = KitchenObj;
                        if (plateKitchenObj.TryAddIngredient(kitchenObj.KitchenObjectSO))
                        {
                            kitchenObj.DestroySelf();

                            _curState = StoveCounterState.Idle;

                            Bootstrap.Instance.EventMgr.ChangeStoveCounterState?.Invoke(gameObject.GetInstanceID(), _curState);
                            Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), _config.ProgressMin);
                        }
                    }
                }
                else
                {
                    KitchenObj.SetCurKitchenObjParent(playerController);

                    _curState = StoveCounterState.Idle;

                    Bootstrap.Instance.EventMgr.ChangeStoveCounterState?.Invoke(gameObject.GetInstanceID(), _curState);
                    Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), _config.ProgressMin);
                }
            }
            else
            {
                if (playerController.HasKitchenObj && HasReceiptWithInput(playerController.KitchenObj.KitchenObjectSO))
                {
                    playerController.KitchenObj.SetCurKitchenObjParent(this);
                    _fryingReceiptSO = GetFryingReceiptSOWithInput(KitchenObj.KitchenObjectSO);

                    _fryingTimer = _config.FryingTimerMin;
                    _curState = StoveCounterState.Frying;

                    Bootstrap.Instance.EventMgr.ChangeStoveCounterState?.Invoke(gameObject.GetInstanceID(), _curState);

                    float progressNormalized = _fryingTimer / _fryingReceiptSO.FryingTimeMax;
                    Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), progressNormalized);
                }
            }
        }

        private bool HasReceiptWithInput(KitchenObjectSO inputKitchenObjSO)
        {
            return GetFryingReceiptSOWithInput(inputKitchenObjSO) != null;
        }

        private FryingReceiptSO GetFryingReceiptSOWithInput(KitchenObjectSO inputKitchenObjSO)
        {
            foreach (FryingReceiptSO fryingReceiptSO in _config.FryingReceipts)
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
            foreach (BurningReceiptSO burningReceiptSO in _config.BurningReceipts)
            {
                if (burningReceiptSO.Input == inputKitchenObjSO)
                {
                    return burningReceiptSO;
                }
            }

            return null;
        }
    }
}
