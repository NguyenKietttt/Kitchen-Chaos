using System;

public sealed class EventManager
{
    public Action OnInteractAction;
    public Action<BaseCounter> OnSelectCounter;
}
