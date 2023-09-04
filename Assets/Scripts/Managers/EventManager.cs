using System;

public sealed class EventManager
{
    public Action Interact;
    public Action CuttingInteract;
    public Action<BaseCounter> SelectCounter;
    public Action<StoveCounter.State> ChangeStoveCounterState;
    public Action<float, int> UpdateCounterProgress;
}
