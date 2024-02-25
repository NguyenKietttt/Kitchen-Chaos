using UnityEngine;
using UnityServiceLocator;

namespace KitchenChaos
{
    public abstract class BaseCounter : MonoBehaviour, IKitchenObjParent, IMainInteractHandler
    {
        public Transform SpawnPoint => _spawnPoint;
        public KitchenObject KitchenObj => _curKitchenObj;
        public bool HasKitchenObj => _curKitchenObj != null;

        [Header("Internal Ref")]
        [SerializeField] private GameObject _selectedVisualObj;
        [SerializeField] private Transform _spawnPoint;

        protected EventManager _eventMgr;
        private KitchenObject _curKitchenObj;

        private void Awake()
        {
            RegisterServices();
        }

        protected virtual void Start()
        {
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
            DeregisterServices();
        }

        public virtual void OnMainInteract(PlayerInteraction player) { }

        public void SetKitchenObj(KitchenObject newKitchenObj)
        {
            _curKitchenObj = newKitchenObj;

            if (newKitchenObj != null)
            {
                _eventMgr.PlaceObject?.Invoke();
            }
        }

        public void ClearKitchenObj()
        {
            _curKitchenObj = null;
        }

        protected virtual void RegisterServices()
        {
            _eventMgr = ServiceLocator.Instance.Get<EventManager>();
        }

        protected virtual void DeregisterServices()
        {
            _eventMgr = null;
        }

        protected virtual void SubscribeEvents()
        {
            _eventMgr.SelectCounter += OnCounterSelected;
        }

        protected virtual void UnsubscribeEvents()
        {
            _eventMgr.SelectCounter -= OnCounterSelected;
        }

        private void OnCounterSelected(int senderID)
        {
            if (_selectedVisualObj != null)
            {
                _selectedVisualObj.SetActive(gameObject.GetInstanceID() == senderID);
            }
        }
    }
}
