using System;

namespace KitchenChaos
{
    public sealed partial class EventManager
    {
        public Action<GameState> ChangeGameState;
    }
}
