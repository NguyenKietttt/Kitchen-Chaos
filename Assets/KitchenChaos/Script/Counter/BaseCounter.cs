using System;
using Codice.Client.BaseCommands.Config;
using UnityEngine;

namespace KitchenChaos
{
    public class BaseCounter : MonoBehaviour, IKitchenObjParent
    {

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

        public virtual void OnInteract(PlayerController playerController) { }

        public virtual void OnCuttingInteract(PlayerController playerController) { }

        public Transform GetSpawnPoint()
        {
            return _spawnPoint;
        }

        public KitchenObject GetKitchenObj()
        {
            return _curKitchenObj;
        }

        public void SetKitchenObj(KitchenObject newKitchenObj)
        {
            _curKitchenObj = newKitchenObj;

            if (newKitchenObj != null)
            {
                Bootstrap.Instance.EventMgr.PlaceObject?.Invoke();
            }
        }

        public bool HasKitchenObj()
        {
            return _curKitchenObj != null;
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
