using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KitchenChaos
{
    public sealed class InputManager
    {
        public enum Binding
        {
            MoveUp,
            MoveDown,
            MoveLeft,
            MoveRight,
            Interact,
            Cut,
            GamepadInteract,
            GamepadCut,
        }

        private const string PLAYER_PREFS_BINDING_KEY = "PLAYER_PREFS_BINDING_KEY";

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

        public string GetBidingText(Binding binding)
        {
            return binding switch
            {
                Binding.MoveUp => _playerInputAction.Player.Move.bindings[1].ToDisplayString(),
                Binding.MoveDown => _playerInputAction.Player.Move.bindings[2].ToDisplayString(),
                Binding.MoveLeft => _playerInputAction.Player.Move.bindings[3].ToDisplayString(),
                Binding.MoveRight => _playerInputAction.Player.Move.bindings[4].ToDisplayString(),
                Binding.Interact => _playerInputAction.Player.Interact.bindings[0].ToDisplayString(),
                Binding.Cut => _playerInputAction.Player.CuttingInteract.bindings[0].ToDisplayString(),
                Binding.GamepadInteract => _playerInputAction.Player.Interact.bindings[1].ToDisplayString(),
                Binding.GamepadCut => _playerInputAction.Player.CuttingInteract.bindings[1].ToDisplayString(),
                _ => string.Empty,
            };
        }

        public void RebindBinding(Binding binding, Action onActionRebound)
        {
            _playerInputAction.Disable();

            InputAction inputAction = null;
            int bindingIndex = -1;

            switch (binding)
            {
                case Binding.MoveUp:
                    inputAction = _playerInputAction.Player.Move;
                    bindingIndex = 1;
                    break;
                case Binding.MoveDown:
                    inputAction = _playerInputAction.Player.Move;
                    bindingIndex = 2;
                    break;
                case Binding.MoveLeft:
                    inputAction = _playerInputAction.Player.Move;
                    bindingIndex = 3;
                    break;
                case Binding.MoveRight:
                    inputAction = _playerInputAction.Player.Move;
                    bindingIndex = 4;
                    break;
                case Binding.Interact:
                    inputAction = _playerInputAction.Player.Interact;
                    bindingIndex = 0;
                    break;
                case Binding.Cut:
                    inputAction = _playerInputAction.Player.CuttingInteract;
                    bindingIndex = 0;
                    break;
                case Binding.GamepadInteract:
                    inputAction = _playerInputAction.Player.Interact;
                    bindingIndex = 1;
                    break;
                case Binding.GamepadCut:
                    inputAction = _playerInputAction.Player.CuttingInteract;
                    bindingIndex = 1;
                    break;
            }

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
