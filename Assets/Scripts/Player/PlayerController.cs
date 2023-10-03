using System;
using UnityEngine;

public sealed class PlayerController : MonoBehaviour, IKitchenObjParent
{
    private const float PLAYER_RADIUS = 0.7f;
    private const int PLAYER_HEIGHT = 2;
    private const int MOVING_SPEED = 7;
    private const int ROTATION_SPEED = 10;
    private const int INTERACTION_DISTANCE = 2;

    public static event Action PickSomething;

    public bool IsMoving { get; private set; }

    [Header("Internal Ref")]
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private Transform _kitchenObjHoldPoint;

    [Header("Property")]
    [SerializeField] private LayerMask _counterLayerMask;

    private BaseCounter _selectedCounter;
    private KitchenObject _kitchenObj;
    private Vector3 _lastInteractionDir;

    private void Start()
    {
        Bootstrap.Instance.EventMgr.Interact += OnInteractAction;
        Bootstrap.Instance.EventMgr.CuttingInteract += OnCuttingInteractAction;
    }

    private void Update()
    {
        Vector2 input = Bootstrap.Instance.InputMgr.GetInputVectorNormalized();
        bool canMove = CanMove(input);

        HandleMovement(canMove, input);
        HandleCounterSelection(input);
    }

    private void OnDestroy()
    {
        Bootstrap.Instance.EventMgr.Interact -= OnInteractAction;
        Bootstrap.Instance.EventMgr.CuttingInteract -= OnCuttingInteractAction;

        PickSomething = null;
    }

    private void HandleCounterSelection(Vector2 input)
    {
        Vector3 curPos = transform.position;
        Vector3 playerPos = new(curPos.x, PLAYER_HEIGHT * 0.5f, curPos.z);
        Vector3 moveDir = new(input.x, 0, input.y);

        if (moveDir != Vector3.zero)
        {
            _lastInteractionDir = moveDir;
        }

        if (Physics.Raycast(playerPos, _lastInteractionDir, out RaycastHit hit, INTERACTION_DISTANCE, _counterLayerMask))
        {
            if (hit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (_selectedCounter == null || _selectedCounter != baseCounter)
                {
                    _selectedCounter = baseCounter;
                    Bootstrap.Instance.EventMgr.SelectCounter?.Invoke(_selectedCounter);
                }
            }
            else
            {
                _selectedCounter = null;
                Bootstrap.Instance.EventMgr.SelectCounter?.Invoke(null);
            }
        }
        else
        {
            _selectedCounter = null;
            Bootstrap.Instance.EventMgr.SelectCounter?.Invoke(null);
        }
    }

    private void OnInteractAction()
    {
        if (!Bootstrap.Instance.GameStateMgr.IsGamePlaying())
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
        if (!Bootstrap.Instance.GameStateMgr.IsGamePlaying())
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
            float moveDistance = MOVING_SPEED * Time.deltaTime;

            if (!CanMove(moveDir, moveDistance))
            {
                moveDir = TryMoveHugWall(moveDir, moveDistance);
            }

            if (CanMove(moveDir, moveDistance))
            {
                UpdatePosition(moveDir, moveDistance);
            }

            UpdateRotationByMovingDirection(moveDir);
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }

        _playerAnimator.UpdateWalkingAnim(canMove);
    }

    private Vector3 TryMoveHugWall(Vector3 moveDir, float moveDistance)
    {
        Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;

        if (moveDirX.x != 0 && CanMove(moveDirX, moveDistance))
        {
            return moveDirX;
        }

        Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;

        if (moveDirZ.z != 0 && CanMove(moveDirZ, moveDistance))
        {
            return moveDirZ;
        }

        return moveDir.normalized;
    }

    private bool CanMove(Vector3 moveDir, float moveDistance)
    {
        Vector3 curPos = transform.position;
        return !Physics.CapsuleCast(curPos, curPos + (Vector3.up * PLAYER_HEIGHT), PLAYER_RADIUS, moveDir, moveDistance);
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
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * ROTATION_SPEED);
    }

    public Transform GetSpawnPoint()
    {
        return _kitchenObjHoldPoint;
    }

    public KitchenObject GetKitchenObj()
    {
        return _kitchenObj;
    }

    public void SetKitchenObj(KitchenObject newKitchenObj)
    {
        _kitchenObj = newKitchenObj;

        if (newKitchenObj != null)
        {
            PickSomething?.Invoke();
        }
    }

    public bool HasKitchenObj()
    {
        return _kitchenObj != null;
    }

    public void ClearKitchenObj()
    {
        _kitchenObj = null;
    }
}
