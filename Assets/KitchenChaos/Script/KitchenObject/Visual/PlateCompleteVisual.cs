using KitchenChaos.Utils;
using UnityEngine;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class PlateCompleteVisual : MonoBehaviour
    {
        [Header("Asset Ref")]
        [SerializeField] private KitchenObjSOToGameObj[]? _KitchenObjSOToGameObjs;

        [Header("External Ref")]
        [SerializeField] private PlateKitchenObject? _plateKitchenObj;

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
            DisableCompleteVisual();
            _eventMgr!.AddIngredientSuccess += OnAddIngredientSuccess;
        }

        private void OnDestroy()
        {
            _eventMgr!.AddIngredientSuccess -= OnAddIngredientSuccess;
            DeregisterServices();
        }

        private void DisableCompleteVisual()
        {
            foreach (KitchenObjSOToGameObj kitchenObjSOGameObj in _KitchenObjSOToGameObjs!)
            {
                GameObject? kitchenObj = kitchenObjSOGameObj.GetGameObj();
                kitchenObj!.SetActive(false);
            }
        }

        private void OnAddIngredientSuccess(int senderID, KitchenObjectSO kitchenObjSO)
        {
            if (senderID != _plateKitchenObj!.GetInstanceID())
            {
                return;
            }

            foreach (KitchenObjSOToGameObj kitchenObjSOGameObj in _KitchenObjSOToGameObjs!)
            {
                if (kitchenObjSOGameObj.GetKitchenObjSO() == kitchenObjSO)
                {
                    GameObject? kitchenObj = kitchenObjSOGameObj.GetGameObj();
                    kitchenObj!.SetActive(true);
                }
            }
        }

        private void CheckNullEditorReferences()
        {
            if (_plateKitchenObj == null)
            {
                CustomLog.LogWarning(this, "missing references in editor! (if it's already registered in parent prefab, skip this message!)");
            }

            if (_KitchenObjSOToGameObjs?.Length <= 0)
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
    }
}
