using System;

namespace KitchenChaos
{
    public sealed partial class EventManager
    {
        public Action<int> SelectCounter;
        public Action<int, StoveCounter.State> ChangeStoveCounterState;
        public Action InteractWithCutCounter;
        public Action InteractWithTrashCounter;
    }
}
