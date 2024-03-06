using UnityEngine;

namespace KitchenChaos
{
    public interface IKitchenObjParent
    {
        public Transform SpawnPoint { get; }
        public KitchenObject KitchenObj { get; }
        public bool HasKitchenObj { get; }

        public void SetKitchenObj(KitchenObject? newKitchenObj);

        public void ClearKitchenObj();
    }
}
