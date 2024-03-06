using System.Linq;
using KitchenChaos.Utils;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class StoveCounter : BaseCounter
    {
        public bool IsFried => _curState is StoveCounterState.Fried;

        [Header("Child Config")]
        [SerializeField] private StoveCounterCfg? _config;

        private FryingReceiptSO? _fryingReceiptSO;
        private BurningReceiptSO? _burningReceiptSO;
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

        public override void OnMainInteract(PlayerInteraction player)
        {
            if (HasKitchenObj)
            {
                if (player.HasKitchenObj)
                {
                    if (player.KitchenObj.TryGetPlate(out PlateKitchenObject? plateKitchenObj))
                    {
                        if (plateKitchenObj!.TryAddIngredient(_curKitchenObj!.KitchenObjectSO))
                        {
                            _curKitchenObj!.DestroySelf();

                            _curState = StoveCounterState.Idle;

                            _eventMgr!.ChangeStoveCounterState?.Invoke(gameObject.GetInstanceID(), _curState);
                            _eventMgr!.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), _config!.ProgressMin);
                        }
                    }
                }
                else
                {
                    _curKitchenObj!.SetCurKitchenObjParent(player);

                    _curState = StoveCounterState.Idle;

                    _eventMgr!.ChangeStoveCounterState?.Invoke(gameObject.GetInstanceID(), _curState);
                    _eventMgr!.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), _config!.ProgressMin);
                }
            }
            else
            {
                if (player.HasKitchenObj && HasReceiptWithInput(player.KitchenObj.KitchenObjectSO))
                {
                    player.KitchenObj.SetCurKitchenObjParent(this);
                    _fryingReceiptSO = GetFryingReceiptSOWithInput(KitchenObj!.KitchenObjectSO);

                    _fryingTimer = _config!.FryingTimerMin;
                    _curState = StoveCounterState.Frying;

                    _eventMgr!.ChangeStoveCounterState?.Invoke(gameObject.GetInstanceID(), _curState);

                    float progressNormalized = _fryingTimer / _fryingReceiptSO.FryingTimeMax;
                    _eventMgr!.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), progressNormalized);
                }
            }
        }

        protected override void CheckNullEditorReferences()
        {
            base.CheckNullEditorReferences();

            if (_config == null)
            {
                CustomLog.LogError(this, "missing references in editor!!!");
            }
        }

        private void OnFryingState()
        {
            if (_fryingReceiptSO == null)
            {
                CustomLog.LogError(this, nameof(_fryingReceiptSO), "Somehow is null!!!");
                return;
            }

            _fryingTimer += Time.deltaTime;

            float progressNormalized = _fryingTimer / _fryingReceiptSO.FryingTimeMax;
            _eventMgr!.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), progressNormalized);

            if (_fryingTimer >= _fryingReceiptSO.FryingTimeMax)
            {
                _curKitchenObj!.DestroySelf();
                KitchenObject.SpawnKitchenObj(_fryingReceiptSO.Output, this);

                _burningTimer = _config!.BurningTimerMin;
                _curState = StoveCounterState.Fried;
                _burningReceiptSO = GetBurningReceiptSOWithInput(_curKitchenObj!.KitchenObjectSO);

                _eventMgr!.ChangeStoveCounterState?.Invoke(gameObject.GetInstanceID(), _curState);
            }
        }

        private void OnFriedState()
        {
            if (_burningReceiptSO == null)
            {
                CustomLog.LogError(this, nameof(_burningReceiptSO), "Somehow is null!!!");
                return;
            }

            _burningTimer += Time.deltaTime;

            float progressNormalized = _burningTimer / _burningReceiptSO.BurningTimeMax;
            _eventMgr!.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), progressNormalized);

            if (_burningTimer >= _burningReceiptSO.BurningTimeMax)
            {
                _curKitchenObj!.DestroySelf();
                KitchenObject.SpawnKitchenObj(_burningReceiptSO.Output, this);

                _curState = StoveCounterState.Burned;

                _eventMgr!.ChangeStoveCounterState?.Invoke(gameObject.GetInstanceID(), _curState);
                _eventMgr!.UpdateCounterProgress?.Invoke(gameObject.GetInstanceID(), _config!.ProgressMin);
            }
        }

        private bool HasReceiptWithInput(KitchenObjectSO inputKitchenObjSO)
        {
            return GetFryingReceiptSOWithInput(inputKitchenObjSO) != null;
        }

        private FryingReceiptSO GetFryingReceiptSOWithInput(KitchenObjectSO inputKitchenObjSO)
        {
            return _config!.FryingReceipts.FirstOrDefault(p => p.Input == inputKitchenObjSO);
        }

        private BurningReceiptSO GetBurningReceiptSOWithInput(KitchenObjectSO inputKitchenObjSO)
        {
            return _config!.BurningReceipts.FirstOrDefault(p => p.Input == inputKitchenObjSO);
        }
    }
}
