using KitchenChaos.Utils;
using UnityEngine;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class StoveCounterVisual : MonoBehaviour
    {
        [Header("External Ref")]
        [SerializeField] private GameObject? _stoveCounterObj;

        [Header("Internal Ref")]
        [SerializeField] private GameObject? _stoveOnObj;
        [SerializeField] private GameObject? _particlesObj;

        private EventManager? _eventMgr;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void Awake()
        {
            RegisterServices();
        }

        private void Start()
        {
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
            DeregisterServices();
        }

        private void OnStoveCounterStateChanged(int senderID, StoveCounterState state)
        {
            if (_stoveCounterObj!.GetInstanceID() != senderID)
            {
                return;
            }

            bool isActiveVisual = state is StoveCounterState.Frying or StoveCounterState.Fried;
            _stoveOnObj!.SetActive(isActiveVisual);
            _particlesObj!.SetActive(isActiveVisual);
        }

        private void CheckNullEditorReferences()
        {
            if (_stoveCounterObj == null || _stoveOnObj == null || _particlesObj == null)
            {
                CustomLog.LogError(this, "missing references in editor!!!");
            }
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
            _eventMgr!.ChangeStoveCounterState += OnStoveCounterStateChanged;
        }

        private void UnsubscribeEvents()
        {
            _eventMgr!.ChangeStoveCounterState -= OnStoveCounterStateChanged;
        }
    }
}
