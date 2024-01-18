using UnityEngine;

namespace KitchenChaos
{
    public sealed class StoveCounter : BaseCounter
    {
        public enum State { Idle, Frying, Fried, Burned }

        private const float BURNING_TIME_MIN = 0.0f;
        private const float FRYING_TIME_MIN = 0.0f;
        private const float PROGRESS_MIN = 0.0f;

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

            switch (_curState)
            {
                case State.Frying:
                    OnFryingState();
                    break;
                case State.Fried:
                    OnFriedState();
                    break;
            }
        }

        private void OnFryingState()
        {
            _fryingTimer += Time.deltaTime;

            float progressNormalized = _fryingTimer / _fryingReceiptSO.FryingTimeMax;
            Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(progressNormalized, gameObject.GetInstanceID());

            if (_fryingTimer >= _fryingReceiptSO.FryingTimeMax)
            {
                GetKitchenObj().DestroySelf();
                KitchenObject.SpawnKitchenObj(_fryingReceiptSO.Output, this);

                _burningTimer = BURNING_TIME_MIN;
                _curState = State.Fried;
                _burningReceiptSO = GetBurningReceiptSOWithInput(GetKitchenObj().GetKitchenObjectSO());

                Bootstrap.Instance.EventMgr.ChangeStoveCounterState?.Invoke(_curState, gameObject.GetInstanceID());
            }
        }

        private void OnFriedState()
        {
            _burningTimer += Time.deltaTime;

            float progressNormalized = _burningTimer / _burningReceiptSO.BurningTimeMax;
            Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(progressNormalized, gameObject.GetInstanceID());

            if (_burningTimer >= _burningReceiptSO.BurningTimeMax)
            {
                GetKitchenObj().DestroySelf();
                KitchenObject.SpawnKitchenObj(_burningReceiptSO.Output, this);

                _curState = State.Burned;

                Bootstrap.Instance.EventMgr.ChangeStoveCounterState?.Invoke(_curState, gameObject.GetInstanceID());
                Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(PROGRESS_MIN, gameObject.GetInstanceID());
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
                            Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(PROGRESS_MIN, gameObject.GetInstanceID());
                        }
                    }
                }
                else
                {
                    GetKitchenObj().SetCurKitchenObjParent(playerController);

                    _curState = State.Idle;

                    Bootstrap.Instance.EventMgr.ChangeStoveCounterState?.Invoke(_curState, gameObject.GetInstanceID());
                    Bootstrap.Instance.EventMgr.UpdateCounterProgress?.Invoke(PROGRESS_MIN, gameObject.GetInstanceID());
                }
            }
            else
            {
                if (playerController.HasKitchenObj() && HasReceiptWithInput(playerController.GetKitchenObj().GetKitchenObjectSO()))
                {
                    playerController.GetKitchenObj().SetCurKitchenObjParent(this);
                    _fryingReceiptSO = GetFryingReceiptSOWithInput(GetKitchenObj().GetKitchenObjectSO());

                    _fryingTimer = FRYING_TIME_MIN;
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
}
