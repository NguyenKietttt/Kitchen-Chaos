using System;

namespace KitchenChaos
{
    public sealed partial class EventManager
    {
        public Action<int, KitchenObjectSO>? AddIngredientSuccess;
        public Action? SpawnReceipt;
        public Action? CompleteReceipt;
    }
}
