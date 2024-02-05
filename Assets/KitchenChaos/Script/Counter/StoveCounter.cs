using UnityEngine;

namespace KitchenChaos
{
    public sealed class StoveCounter : BaseCounter
    {
        private const float BURNING_TIME_MIN = 0.0f;
        private const float FRYING_TIME_MIN = 0.0f;
        private const float PROGRESS_MIN = 0.0f;

        public bool IsFried => _curState is StoveCounterState.Fried;

        [Header("Child Asset Ref")]
        [SerializeField] private FryingReceiptSO[] _fryingReceipts;
        [SerializeField] private BurningReceiptSO[] _burningReceipts;

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

                _burningTimer = BURNING_TIME_MIN;
                _curState = StoveCounterState.Fried;
                _burningReceiptSO = GetBurningReceiptSOWithInput(KitchenObj.GetKitchenObjectSO());

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
                Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), PROGRESS_MIN);
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
                        if (plateKitchenObj.TryAddIngredient(kitchenObj.GetKitchenObjectSO()))
                        {
                            kitchenObj.DestroySelf();

                            _curState = StoveCounterState.Idle;

                            Bootstrap.Instance.EventMgr.ChangeStoveCounterState?.Invoke(gameObject.GetInstanceID(), _curState);
                            Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), PROGRESS_MIN);
                        }
                    }
                }
                else
                {
                    KitchenObj.SetCurKitchenObjParent(playerController);

                    _curState = StoveCounterState.Idle;

                    Bootstrap.Instance.EventMgr.ChangeStoveCounterState?.Invoke(gameObject.GetInstanceID(), _curState);
                    Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), PROGRESS_MIN);
                }
            }
            else
            {
                if (playerController.HasKitchenObj && HasReceiptWithInput(playerController.KitchenObj.GetKitchenObjectSO()))
                {
                    playerController.KitchenObj.SetCurKitchenObjParent(this);
                    _fryingReceiptSO = GetFryingReceiptSOWithInput(KitchenObj.GetKitchenObjectSO());

                    _fryingTimer = FRYING_TIME_MIN;
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
            foreach (FryingReceiptSO fryingReceiptSO in _fryingReceipts)
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
            foreach (BurningReceiptSO burningReceiptSO in _burningReceipts)
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
