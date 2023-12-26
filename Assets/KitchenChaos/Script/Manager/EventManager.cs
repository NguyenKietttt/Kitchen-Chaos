using System;

public sealed class EventManager
{
    public Action<GameState> ChangeGameState;
    public Action Interact;
    public Action CuttingInteract;
    public Action TooglePause;
    public Action OnPaused;
    public Action OnUnPaused;
    public Action RebindingKey;
    public Action<BaseCounter> SelectCounter;
    public Action<StoveCounter.State, int> ChangeStoveCounterState;
    public Action SpawnPlate;
    public Action RemovePlate;
    public Action<int, KitchenObjectSO> AddIngredientSuccess;
    public Action<float, int> UpdateCounterProgress;
    public Action SpawnReceipt;
    public Action CompleteReceipt;

    // Sounds
    public Action DeliverReceiptSuccess;
    public Action DeliverReceiptFailed;
    public Action CountdownPopup;
    public Action StoveWarning;

    // UI
    public Action ClickOptionsBtn;
    public Action CloseOptionUI;
}
