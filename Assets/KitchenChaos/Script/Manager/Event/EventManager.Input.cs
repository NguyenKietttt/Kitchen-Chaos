using System;

namespace KitchenChaos
{
    public sealed partial class EventManager
    {
        // Input
        public Action Interact;
        public Action CuttingInteract;
        public Action TogglePause;
        public Action RebindKey;
        public Action PickSomething;
    }
}
