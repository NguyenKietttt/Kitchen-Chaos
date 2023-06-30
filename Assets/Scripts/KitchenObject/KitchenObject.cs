using System;
using UnityEngine;

public sealed class KitchenObject : MonoBehaviour
{
   [SerializeField] private KitchenObjectSO _kitchenObjectS0;

   private Transform _transform;
   private ClearCounter _curClearCounter;

   private void Awake()
   {
      _transform = transform;
   }

   public KitchenObjectSO GetKitchenObjectSO()
   {
      return _kitchenObjectS0;
   }

   public ClearCounter GetCurClearCounter()
   {
      return _curClearCounter;
   }

   public void SetCurClearCounter(ClearCounter newClearCounter)
   {
      if (_curClearCounter != null)
      {
         _curClearCounter.SetKitchenObj(null);
      }

      _curClearCounter = newClearCounter;
      _curClearCounter.SetKitchenObj(this);
      _transform.parent = newClearCounter.GetCounterSpawnPoint();
      _transform.localPosition = Vector3.zero;
   }
}
