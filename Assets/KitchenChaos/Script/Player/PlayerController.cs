using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlayerController : MonoBehaviour, IKitchenObjParent
    {
        public Transform SpawnPoint => _kitchenObjHoldPoint;
        public KitchenObject KitchenObj => _kitchenObj;
        public bool HasKitchenObj => _kitchenObj != null;
        public bool IsMoving => _isMoving;

        [Header("Config")]
        [SerializeField] private PlayerControllerCfg _config;

        [Header("Internal Ref")]
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private Transform _kitchenObjHoldPoint;

        private BaseCounter _selectedCounter;
        private KitchenObject _kitchenObj;
        private GameState _curState;
        private Vector3 _lastInteractionDir;
        private bool _isMoving;

        private void OnEnable()
        {
            Bootstrap.Instance.EventMgr.ChangeGameState += OnGameStateChanged;
            Bootstrap.Instance.EventMgr.Interact += OnInteractAction;
            Bootstrap.Instance.EventMgr.CuttingInteract += OnCuttingInteractAction;
        }

        private void Update()
        {
            Vector2 input = Bootstrap.Instance.InputMgr.InputVectorNormalized;
            bool canMove = CanMove(input);

            HandleMovement(canMove, input);
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
            if (_curState is not GameState.GamePlaying)
            {
                return;
            }

            if (_selectedCounter != null)
            {
                _selectedCounter.OnInteract(this);
            }
        }

        private void OnCuttingInteractAction()
        {
            if (_curState is not GameState.GamePlaying)
            {
                return;
            }

            if (_selectedCounter != null)
            {
                _selectedCounter.OnCuttingInteract(this);
            }
        }

        private void HandleMovement(bool canMove, Vector2 input)
        {
            if (canMove)
            {
                Vector3 moveDir = new(input.x, 0, input.y);
                float moveDistance = _config.MoveSpeed * Time.deltaTime;

                if (!CanMove(moveDir, moveDistance))
                {
                    moveDir = TryMoveHugWall(moveDir, moveDistance);
                }

                if (CanMove(moveDir, moveDistance))
                {
                    UpdatePosition(moveDir, moveDistance);
                }

                UpdateRotationByMovingDirection(moveDir);
                _isMoving = true;
            }
            else
            {
                _isMoving = false;
            }

            _playerAnimator.UpdateWalkingAnim(canMove);
        }

        private Vector3 TryMoveHugWall(Vector3 moveDir, float moveDistance)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;

            if ((moveDirX.x <= -_config.MoveOffset || moveDirX.x >= _config.MoveOffset) && CanMove(moveDirX, moveDistance))
            {
                return moveDirX;
            }

            Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;

            if ((moveDirZ.z <= -_config.MoveOffset || moveDirZ.z >= _config.MoveOffset) && CanMove(moveDirZ, moveDistance))
            {
                return moveDirZ;
            }

            return moveDir.normalized;
        }

        private bool CanMove(Vector3 moveDir, float moveDistance)
        {
            Vector3 curPos = transform.position;
            return !Physics.CapsuleCast(curPos, curPos + (Vector3.up * _config.Height), _config.Radius, moveDir, moveDistance);
        }

        public bool CanMove(Vector2 input)
        {
            return input != Vector2.zero;
        }

        private void UpdatePosition(Vector3 moveDir, float moveDistance)
        {
            transform.position += moveDir * moveDistance;
        }

        private void UpdateRotationByMovingDirection(Vector3 moveDir)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _config.RotateSpeed);
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
    }
}
