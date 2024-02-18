using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlatesCounter : BaseCounter
    {
        [Header("Child Config")]
        [SerializeField] private PlatesCounterCfg _config;

        private GameState _curState;
        private float _spawnPlateTimer;
        private int _platesSpawnAmount;

        private void OnEnable()
        {
            Bootstrap.Instance.EventMgr.ChangeGameState += OnGameStateChanged;
        }

        private void Update()
        {
            _spawnPlateTimer += Time.deltaTime;
            if (_spawnPlateTimer >= _config.PlateSpawnTimerMax)
            {
                _spawnPlateTimer = _config.PlateSpawnTimerMin;
                if (_curState is GameState.GamePlaying && _platesSpawnAmount < _config.PlateSpawnAmountMax)
                {
                    _platesSpawnAmount++;
                    Bootstrap.Instance.EventMgr.SpawnPlate?.Invoke();
                }
            }
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.ChangeGameState -= OnGameStateChanged;
        }

        public override void OnInteract(PlayerInteraction player)
        {
            if (!player.HasKitchenObj && _platesSpawnAmount > _config.PlateSpawnTimerMin)
            {
                _platesSpawnAmount--;

                KitchenObject.SpawnKitchenObj(_config.PlateSO, player);
                Bootstrap.Instance.EventMgr.RemovePlate?.Invoke();
            }
        }

        private void OnGameStateChanged(GameState state)
        {
            _curState = state;
        }
    }
}
