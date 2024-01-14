using UnityEngine;

namespace KitchenChaos
{
    public class KitchenObject : MonoBehaviour
    {
        [Header("SO")]
        [SerializeField] private KitchenObjectSO _kitchenObjectS0;

        private IKitchenObjParent _curKitchenObjParent;

        public static KitchenObject SpawnKitchenObj(KitchenObjectSO kitchenObjectSO, IKitchenObjParent kitchenObjParent)
        {
            Transform kitchenObjTrans = Instantiate(kitchenObjectSO.Prefab).transform;
            KitchenObject kitchenObj = kitchenObjTrans.GetComponent<KitchenObject>();
            kitchenObj.SetCurKitchenObjParent(kitchenObjParent);

            return kitchenObj;
        }

        public KitchenObjectSO GetKitchenObjectSO()
        {
            return _kitchenObjectS0;
        }

        public void SetCurKitchenObjParent(IKitchenObjParent newKitchenObjParent)
        {
            _curKitchenObjParent?.SetKitchenObj(null);
            _curKitchenObjParent = newKitchenObjParent;

            if (_curKitchenObjParent.HasKitchenObj())
            {
                Debug.Log("IKitchenObjParent already has a KitchenObj!");
            }

            _curKitchenObjParent.SetKitchenObj(this);
            transform.parent = newKitchenObjParent.GetSpawnPoint();
            transform.localPosition = Vector3.zero;
        }

        public void DestroySelf()
        {
            _curKitchenObjParent.ClearKitchenObj();
            Destroy(gameObject);
        }

        public bool TryGetPlate(out PlateKitchenObject plateKitchenObj)
        {
            if (this is PlateKitchenObject curPlateKitchenObj)
            {
                plateKitchenObj = curPlateKitchenObj;
                return true;
            }
            else
            {
                plateKitchenObj = null;
                return false;
            }
        }
    }
}
