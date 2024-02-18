using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlayerInteraction : MonoBehaviour, IKitchenObjParent
    {
        public Transform SpawnPoint => _kitchenObjHoldPoint;
        public KitchenObject KitchenObj => _kitchenObj;
        public bool HasKitchenObj => _kitchenObj != null;

        [Header("Config")]
        [SerializeField] private PlayerCfg _config;

        [Header("Internal Ref")]
        [SerializeField] private Transform _kitchenObjHoldPoint;

        private BaseCounter _selectedCounter;
        private KitchenObject _kitchenObj;
        private GameState _curState;
        private Vector3 _lastInteractionDir;

        private void OnEnable()
        {
            Bootstrap.Instance.EventMgr.ChangeGameState += OnGameStateChanged;
            Bootstrap.Instance.EventMgr.Interact += OnInteractAction;
            Bootstrap.Instance.EventMgr.CuttingInteract += OnCuttingInteractAction;
        }

        private void Update()
        {
            Vector2 input = Bootstrap.Instance.InputMgr.InputVectorNormalized;
            HandleCounterSelection(input);
        }

        private void OnDisable()
        {
            Bootstrap.Instance.EventMgr.ChangeGameState -= OnGameStateChanged;
            Bootstrap.Instance.EventMgr.Interact -= OnInteractAction;
            Bootstrap.Instance.EventMgr.CuttingInteract -= OnCuttingInteractAction;
        }

        private void HandleCounterSelection(Vector2 input)
        {
            Vector3 curPos = transform.position;
            Vector3 playerPos = new(curPos.x, _config.Height * _config.HeightOffset, curPos.z);
            Vector3 moveDir = new(input.x, 0, input.y);

            if (moveDir != Vector3.zero)
            {
                _lastInteractionDir = moveDir;
            }

            if (Physics.Raycast(playerPos, _lastInteractionDir, out RaycastHit hit, _config.InteractDistance, _config.CounterLayerMask))
            {
                if (hit.transform.TryGetComponent(out BaseCounter baseCounter))
                {
                    if (_selectedCounter == null || _selectedCounter != baseCounter)
                    {
                        _selectedCounter = baseCounter;
                        Bootstrap.Instance.EventMgr.SelectCounter?.Invoke(_selectedCounter.gameObject.GetInstanceID());
                    }
                }
                else
                {
                    _selectedCounter = null;
                    Bootstrap.Instance.EventMgr.SelectCounter?.Invoke(int.MinValue);
                }
            }
            else
            {
                _selectedCounter = null;
                Bootstrap.Instance.EventMgr.SelectCounter?.Invoke(int.MinValue);
            }
        }

        private void OnInteractAction()
        {
            if (CanInteract())
            {
                _selectedCounter.OnInteract(this);
            }

        }

        private void OnCuttingInteractAction()
        {
            if (CanInteract())
            {
                _selectedCounter.OnCuttingInteract();
            }
        }

        public void SetKitchenObj(KitchenObject newKitchenObj)
        {
            _kitchenObj = newKitchenObj;

            if (newKitchenObj != null)
            {
                Bootstrap.Instance.EventMgr.PickSomething?.Invoke();
            }
        }

        public void ClearKitchenObj()
        {
            _kitchenObj = null;
        }

        private void OnGameStateChanged(GameState state)
        {
            _curState = state;
        }

        private bool CanInteract()
        {
            return _curState is GameState.GamePlaying && _selectedCounter != null;
        }
    }
}
