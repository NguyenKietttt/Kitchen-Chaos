using System;

public sealed class EventManager
{
    public Action ChangeGameState;
    public Action Interact;
    public Action CuttingInteract;
    public Action TooglePause;
    public Action OnPaused;
    public Action OnUnPaused;
    public Action<BaseCounter> SelectCounter;
    public Action<StoveCounter.State> ChangeStoveCounterState;
    public Action SpawnPlate;
    public Action RemovePlate;
    public Action<int, KitchenObjectSO> AddIngredientSuccess;
    public Action<float, int> UpdateCounterProgress;
    public Action SpawnReceipt;
    public Action CompleteReceipt;

    // Sounds
    public Action DeliverReceiptSuccess;
    public Action DeliverReceiptFailed;

    // UI
    public Action ClickOptionsBtn;
    public Action CloseOptionUI;

    public void Dispose()
    {
        Interact = delegate { };
        CuttingInteract = delegate { };
        OnPaused = delegate { };
        OnUnPaused = delegate { };
        SelectCounter = delegate { };
        ChangeStoveCounterState = delegate { };
        SpawnPlate = delegate { };
        RemovePlate = delegate { };
        AddIngredientSuccess = delegate { };
        UpdateCounterProgress = delegate { };
        SpawnReceipt = delegate { };
        CompleteReceipt = delegate { };
    }
}
