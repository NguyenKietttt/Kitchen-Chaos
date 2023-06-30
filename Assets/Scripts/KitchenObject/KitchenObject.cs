using UnityEngine;

public sealed class KitchenObject : MonoBehaviour
{
   [SerializeField] private KitchenObjectSO _kitchenObjectS0;

   public KitchenObjectSO GetKitchenObjectSO()
   {
      return _kitchenObjectS0;
   }
}
