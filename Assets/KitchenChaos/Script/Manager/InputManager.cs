using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class InputManager : MonoBehaviour
    {
        private const string PLAYER_PREFS_BINDING_KEY = "PLAYER_PREFS_BINDING_KEY";

        public PlayerInputAction PlayerInputAction => _playerInputAction;
        public Vector2 InputVectorNormalized => _playerInputAction.Player.Move.ReadValue<Vector2>();

        private EventManager _eventMgr;

        private PlayerInputAction _playerInputAction;

        public void Init()
        {
            RegisterServices();
            InitPlayerInputAction();
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            _playerInputAction.Dispose();
            UnsubscribeEvents();
            DeregisterServices();
        }

        public void RebindBinding(string actionName, int bindingIndex, Action onActionRebound)
        {
            _playerInputAction.Disable();

            InputAction inputAction = _playerInputAction.asset.FindAction(actionName);

            inputAction.PerformInteractiveRebinding(bindingIndex)
                .OnComplete(callback =>
                {
                    callback.Dispose();
                    _playerInputAction.Enable();

                    onActionRebound?.Invoke();

                    PlayerPrefs.SetString(PLAYER_PREFS_BINDING_KEY, _playerInputAction.SaveBindingOverridesAsJson());
                    PlayerPrefs.Save();

                    _eventMgr.RebindKey?.Invoke();
                })
                .Start();
        }

        private void InitPlayerInputAction()
        {
            _playerInputAction = new PlayerInputAction();

            if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDING_KEY))
            {
                _playerInputAction.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDING_KEY));
            }

            _playerInputAction.Enable();
        }

        private void OnInteractPerformed(InputAction.CallbackContext obj)
        {
            _eventMgr.Interact?.Invoke();
        }

        private void OnCuttingInteractPerformed(InputAction.CallbackContext obj)
        {
            _eventMgr.CuttingInteract?.Invoke();
        }

        private void OnPausePerformed(InputAction.CallbackContext obj)
        {
            _eventMgr.TogglePause?.Invoke();
        }

        private void RegisterServices()
        {
            _eventMgr = ServiceLocator.Instance.Get<EventManager>();
        }

        private void DeregisterServices()
        {
            _eventMgr = null;
        }

        private void SubscribeEvents()
        {
            _playerInputAction.Player.Interact.performed += OnInteractPerformed;
            _playerInputAction.Player.CuttingInteract.performed += OnCuttingInteractPerformed;
            _playerInputAction.Player.Pause.performed += OnPausePerformed;
        }

        private void UnsubscribeEvents()
        {
            _playerInputAction.Player.Interact.performed -= OnInteractPerformed;
            _playerInputAction.Player.CuttingInteract.performed -= OnCuttingInteractPerformed;
            _playerInputAction.Player.Pause.performed -= OnPausePerformed;
        }
    }
}
