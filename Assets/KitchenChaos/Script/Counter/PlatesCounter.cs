using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlatesCounter : BaseCounter
    {
        private const float SPAWN_PLATE_TIMER_MAX = 4.0f;
        private const int PLATES_SPAWN_AMOUNT_MAX = 4;

        [Header("Child Asset Ref")]
        [SerializeField] private KitchenObjectSO _plateKitchenObjSO;

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

            if (_spawnPlateTimer >= SPAWN_PLATE_TIMER_MAX)
            {
                _spawnPlateTimer = 0;

                if (_curState is GameState.GamePlaying && _platesSpawnAmount < PLATES_SPAWN_AMOUNT_MAX)
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

        public override void OnInteract(PlayerController playerController)
        {
            if (!playerController.HasKitchenObj() && _platesSpawnAmount > 0)
            {
                _platesSpawnAmount--;

                KitchenObject.SpawnKitchenObj(_plateKitchenObjSO, playerController);
                Bootstrap.Instance.EventMgr.RemovePlate?.Invoke();
            }
        }

        private void OnGameStateChanged(GameState state)
        {
            _curState = state;
        }
    }
}
