using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KitchenChaos
{
    public sealed class InputManager
    {
        private const string PLAYER_PREFS_BINDING_KEY = "PLAYER_PREFS_BINDING_KEY";

        public PlayerInputAction PlayerInputAction => _playerInputAction;
        public Vector2 InputVectorNormalized => _playerInputAction.Player.Move.ReadValue<Vector2>();

        private PlayerInputAction _playerInputAction;

        public InputManager()
        {
            InitPlayerInputAction();
            SubscribeEvents();
        }

        public void OnDestroy()
        {
            UnsubscribeEvents();
            _playerInputAction.Dispose();
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

                    Bootstrap.Instance.EventMgr.RebindingKey?.Invoke();
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

        private void OnInteractPerformed(InputAction.CallbackContext obj)
        {
            Bootstrap.Instance.EventMgr.Interact?.Invoke();
        }

        private void OnCuttingInteractPerformed(InputAction.CallbackContext obj)
        {
            Bootstrap.Instance.EventMgr.CuttingInteract?.Invoke();
        }

        private void OnPausePerformed(InputAction.CallbackContext obj)
        {
            Bootstrap.Instance.EventMgr.TooglePause?.Invoke();
        }
    }
}
