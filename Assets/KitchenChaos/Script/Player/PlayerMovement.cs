using UnityEngine;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class PlayerMovement : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private PlayerCfg _config;

        [Header("Internal Ref")]
        [SerializeField] private PlayerAnimator _playerAnimator;

        private EventManager _eventMgr;
        private InputManager _inputMgr;

        private void Awake()
        {
            RegisterServices();
        }

        private void Update()
        {
            Vector2 input = _inputMgr.InputVectorNormalized;
            HandleMovement(input);
        }

        private void OnDestroy()
        {
            DeregisterServices();
        }

        private void HandleMovement(Vector2 input)
        {
            if (input != Vector2.zero)
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
                _eventMgr.PlayerMove?.Invoke();
            }
            else
            {
                _eventMgr.PlayerStop?.Invoke();
            }

            _playerAnimator.UpdateWalkingAnim(input != Vector2.zero);
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

        private void UpdatePosition(Vector3 moveDir, float moveDistance)
        {
            transform.position += moveDir * moveDistance;
        }

        private void UpdateRotationByMovingDirection(Vector3 moveDir)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _config.RotateSpeed);
        }

        private bool CanMove(Vector3 moveDir, float moveDistance)
        {
            Vector3 curPos = transform.position;
            return !Physics.CapsuleCast(curPos, curPos + (Vector3.up * _config.Height), _config.Radius, moveDir, moveDistance);
        }

        private void RegisterServices()
        {
            _eventMgr = ServiceLocator.Instance.Get<EventManager>();
            _inputMgr = ServiceLocator.Instance.Get<InputManager>();
        }

        private void DeregisterServices()
        {
            _eventMgr = null;
            _inputMgr = null;
        }
    }
}
