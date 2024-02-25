using UnityEngine;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class PlateCompleteVisual : MonoBehaviour
    {
        [Header("Asset Ref")]
        [SerializeField] private KitchenObjSOToGameObj[] _KitchenObjSOToGameObjs;

        [Header("External Ref")]
        [SerializeField] private PlateKitchenObject _plateKitchenObj;

        private EventManager _eventMgr;

        private void Awake()
        {
            RegisterServices();
        }

        private void Start()
        {
            DisableCompleteVisual();
            _eventMgr.AddIngredientSuccess += OnAddIngredientSuccess;
        }

        private void OnDestroy()
        {
            _eventMgr.AddIngredientSuccess -= OnAddIngredientSuccess;
            DeregisterServices();
        }

        private void DisableCompleteVisual()
        {
            foreach (KitchenObjSOToGameObj kitchenObjSOGameObj in _KitchenObjSOToGameObjs)
            {
                kitchenObjSOGameObj.GameObj.SetActive(false);
            }
        }

        private void OnAddIngredientSuccess(int senderID, KitchenObjectSO kitchenObjSO)
        {
            if (senderID != _plateKitchenObj.GetInstanceID())
            {
                return;
            }

            foreach (KitchenObjSOToGameObj kitchenObjSOGameObj in _KitchenObjSOToGameObjs)
            {
                if (kitchenObjSOGameObj.KitchenObjSO == kitchenObjSO)
                {
                    kitchenObjSOGameObj.GameObj.SetActive(true);
                }
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
    }
}
