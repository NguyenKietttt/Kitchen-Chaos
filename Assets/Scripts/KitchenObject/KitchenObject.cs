using UnityEngine;

public sealed class KitchenObject : MonoBehaviour
{
   [SerializeField] private KitchenObjectSO _kitchenObjectS0;

   private Transform _transform;
   private IKitchenObjParent _curKitchenObjParent;

   private void Awake()
   {
      _transform = transform;
   }

   public KitchenObjectSO GetKitchenObjectSO()
   {
      return _kitchenObjectS0;
   }

   public IKitchenObjParent GetKitchenObjParent()
   {
      return _curKitchenObjParent;
   }

   public void SetCurKitchenObjParent(IKitchenObjParent newKitchenObjParent)
   {
      if (_curKitchenObjParent != null)
      {
         _curKitchenObjParent.SetKitchenObj(null);
      }

      _curKitchenObjParent = newKitchenObjParent;

      if (_curKitchenObjParent.HasKitchenObj())
      {
         Debug.Log("IKitchenObjParent already has a KitchenObj!");
      }

      _curKitchenObjParent.SetKitchenObj(this);
      _transform.parent = newKitchenObjParent.GetSpawnPoint();
      _transform.localPosition = Vector3.zero;
   }
}