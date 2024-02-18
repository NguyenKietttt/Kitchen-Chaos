using UnityEngine;

namespace KitchenChaos
{
    public abstract class BaseCounter : MonoBehaviour, IKitchenObjParent
    {
        public Transform SpawnPoint => _spawnPoint;
        public KitchenObject KitchenObj => _curKitchenObj;
        public bool HasKitchenObj => _curKitchenObj != null;

        [Header("Internal Ref")]
        [SerializeField] private GameObject _selectedVisualObj;
        [SerializeField] private Transform _spawnPoint;

        private KitchenObject _curKitchenObj;

        protected virtual void Start()
        {
            Bootstrap.Instance.EventMgr.SelectCounter += OnCounterSelected;
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.SelectCounter -= OnCounterSelected;
        }

        public virtual void OnInteract(PlayerInteraction player) { }

        public virtual void OnCuttingInteract() { }

        public void SetKitchenObj(KitchenObject newKitchenObj)
        {
            _curKitchenObj = newKitchenObj;

            if (newKitchenObj != null)
            {
                Bootstrap.Instance.EventMgr.PlaceObject?.Invoke();
            }
        }

        public void ClearKitchenObj()
        {
            _curKitchenObj = null;
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
