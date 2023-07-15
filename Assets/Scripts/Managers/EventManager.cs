using System;

public sealed class EventManager
{
    public Action OnInteractAction;
    public Action OnCuttingInteractAction;
    public Action<BaseCounter> OnSelectCounter;
}
