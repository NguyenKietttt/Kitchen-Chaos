using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlateCompleteVisual : MonoBehaviour
    {
        [Header("External Ref")]
        [SerializeField] private PlateKitchenObject _plateKitchenObj;

        [Header("Mix Ref")]
        [SerializeField]
        private KitchenObjSO_GameObj[] _listKitchenObjSOGameObj;

        private void Start()
        {
            DeactiveCompleteVisual();

            Bootstrap.Instance.EventMgr.AddIngredientSuccess += OnAddIngredientSucces;
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.AddIngredientSuccess -= OnAddIngredientSucces;
        }

        private void DeactiveCompleteVisual()
        {
            for (int i = 0; i < _listKitchenObjSOGameObj.Length; i++)
            {
                _listKitchenObjSOGameObj[i].GameObj.SetActive(false);
            }
        }

        private void OnAddIngredientSucces(int plateID, KitchenObjectSO kitchenObjSO)
        {
            if (plateID != _plateKitchenObj.GetInstanceID())
            {
                return;
            }

            for (int i = 0; i < _listKitchenObjSOGameObj.Length; i++)
            {
                KitchenObjSO_GameObj kitchenObjSOGameObj = _listKitchenObjSOGameObj[i];
                if (kitchenObjSOGameObj.KitchenObjSO == kitchenObjSO)
                {
                    kitchenObjSOGameObj.GameObj.SetActive(true);
                }
            }
        }
    }
}
