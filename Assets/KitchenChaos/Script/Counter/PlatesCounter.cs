using KitchenChaos.Utils;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlatesCounter : BaseCounter
    {
        [Header("Child Config")]
        [SerializeField] private PlatesCounterCfg? _config;

        private GameState _curState;
        private float _spawnPlateTimer;
        private int _platesSpawnAmount;

        private void Update()
        {
            _spawnPlateTimer += Time.deltaTime;
            if (_spawnPlateTimer >= _config!.PlateSpawnTimerMax)
            {
                _spawnPlateTimer = _config.PlateSpawnTimerMin;
                if (_curState is GameState.GamePlaying && _platesSpawnAmount < _config.PlateSpawnAmountMax)
                {
                    _platesSpawnAmount++;
                    _eventMgr!.SpawnPlate?.Invoke();
                }
            }
        }

        protected override void CheckNullEditorReferences()
        {
            base.CheckNullEditorReferences();

            if (_config == null)
            {
                CustomLog.LogError(this, "missing internal references in editor!");
            }
        }

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            _eventMgr!.ChangeGameState += OnGameStateChanged;
        }

        protected override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();
            _eventMgr!.ChangeGameState -= OnGameStateChanged;
        }

        public override void OnMainInteract(PlayerInteraction player)
        {
            if (!player.HasKitchenObj && _platesSpawnAmount > _config!.PlateSpawnTimerMin)
            {
                _platesSpawnAmount--;

                KitchenObject.SpawnKitchenObj(_config.PlateSO, player);
                _eventMgr!.RemovePlate?.Invoke();
            }
        }

        private void OnGameStateChanged(GameState state)
        {
            _curState = state;
        }
    }
}
