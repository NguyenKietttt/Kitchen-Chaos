using UnityEngine;

public interface IKitchenObjParent
{
    public Transform GetSpawnPoint();

    public KitchenObject GetKitchenObj();

    public void SetKitchenObj(KitchenObject newKitchenObj);

    public bool HasKitchenObj();

    public void ClearKitchenObj();
}
