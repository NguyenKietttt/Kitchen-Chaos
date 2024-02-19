using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlateCompleteVisual : MonoBehaviour
    {
        [Header("Asset Ref")]
        [SerializeField] private KitchenObjSOToGameObj[] _KitchenObjSOToGameObjs;

        [Header("External Ref")]
        [SerializeField] private PlateKitchenObject _plateKitchenObj;

        private void Start()
        {
            DisableCompleteVisual();

            Bootstrap.Instance.EventMgr.AddIngredientSuccess += OnAddIngredientSuccess;
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.AddIngredientSuccess -= OnAddIngredientSuccess;
        }

        private void DisableCompleteVisual()
        {
            for (int i = 0; i < _KitchenObjSOToGameObjs.Length; i++)
            {
                _KitchenObjSOToGameObjs[i].GameObj.SetActive(false);
            }
        }

        private void OnAddIngredientSuccess(int senderID, KitchenObjectSO kitchenObjSO)
        {
            if (senderID != _plateKitchenObj.GetInstanceID())
            {
                return;
            }

            for (int i = 0; i < _KitchenObjSOToGameObjs.Length; i++)
            {
                KitchenObjSOToGameObj kitchenObjSOGameObj = _KitchenObjSOToGameObjs[i];
                if (kitchenObjSOGameObj.KitchenObjSO == kitchenObjSO)
                {
                    kitchenObjSOGameObj.GameObj.SetActive(true);
                }
            }
        }
    }
}
