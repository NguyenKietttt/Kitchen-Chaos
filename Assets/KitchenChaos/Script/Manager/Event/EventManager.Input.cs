using System;

namespace KitchenChaos
{
    public sealed partial class EventManager
    {
        // Input
        public Action Interact;
        public Action CuttingInteract;
        public Action TooglePause;
        public Action RebindingKey;
        public Action PickSomething;
    }
}
