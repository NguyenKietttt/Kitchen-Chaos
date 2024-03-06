using KitchenChaos.Utils;
using UnityEngine;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class PlayerInteraction : MonoBehaviour, IKitchenObjParent
    {
        public Transform SpawnPoint => _kitchenObjHoldPoint!;
        public KitchenObject KitchenObj => _kitchenObj!;
        public bool HasKitchenObj => _kitchenObj != null;

        [Header("Config")]
        [SerializeField] private PlayerCfg? _config;

        [Header("Internal Ref")]
        [SerializeField] private Transform? _kitchenObjHoldPoint;

        private EventManager? _eventMgr;
        private InputManager? _inputMgr;

        private BaseCounter? _selectedCounter;
        private KitchenObject? _kitchenObj;
        private GameState _curState;
        private Vector3 _lastInteractionDir;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void Awake()
        {
            RegisterServices();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Update()
        {
            Vector2 input = _inputMgr!.InputVectorNormalized;
            HandleCounterSelection(input);
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void OnDestroy()
        {
            DeregisterServices();
        }

        private void HandleCounterSelection(Vector2 input)
        {
            Vector3 curPos = transform.position;
            Vector3 playerPos = new(curPos.x, _config!.Height * _config.HeightOffset, curPos.z);
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
                        _eventMgr!.SelectCounter?.Invoke(_selectedCounter.gameObject.GetInstanceID());
                    }
                }
                else
                {
                    _selectedCounter = null;
                    _eventMgr!.SelectCounter?.Invoke(int.MinValue);
                }
            }
            else
            {
                _selectedCounter = null;
                _eventMgr!.SelectCounter?.Invoke(int.MinValue);
            }
        }

        private void OnInteractAction()
        {
            if (CanInteract() && _selectedCounter!.gameObject.TryGetComponent(out IMainInteractHandler mainInteractHandler))
            {
                mainInteractHandler.OnMainInteract(this);
            }

        }

        private void OnCuttingInteractAction()
        {
            if (CanInteract() && _selectedCounter!.gameObject.TryGetComponent(out ISecondaryInteractHandler secondaryInteractHandler))
            {
                secondaryInteractHandler.OnSecondaryInteract();
            }
        }

        public void SetKitchenObj(KitchenObject? newKitchenObj)
        {
            _kitchenObj = newKitchenObj;

            if (newKitchenObj != null)
            {
                _eventMgr!.PickSomething?.Invoke();
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

        private void CheckNullEditorReferences()
        {
            if (_config == null || _kitchenObjHoldPoint == null)
            {
                CustomLog.LogError(this, "missing internal references in editor!");
            }
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

        private void SubscribeEvents()
        {
            _eventMgr!.ChangeGameState += OnGameStateChanged;
            _eventMgr!.Interact += OnInteractAction;
            _eventMgr!.CuttingInteract += OnCuttingInteractAction;
        }

        private void UnsubscribeEvents()
        {
            _eventMgr!.ChangeGameState -= OnGameStateChanged;
            _eventMgr!.Interact -= OnInteractAction;
            _eventMgr!.CuttingInteract -= OnCuttingInteractAction;
        }
    }
}
