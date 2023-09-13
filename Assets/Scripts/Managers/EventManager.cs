using System;

public sealed class EventManager
{
    public Action Interact;
    public Action CuttingInteract;
    public Action<BaseCounter> SelectCounter;
    public Action<StoveCounter.State> ChangeStoveCounterState;
    public Action SpawnPlate;
    public Action RemovePlate;
    public Action<int, KitchenObjectSO> AddIngredientSuccess;
    public Action<float, int> UpdateCounterProgress;
    public Action SpawnReceipt;
    public Action CompleteReceipt;
}
