using System;

namespace KitchenChaos
{
    public sealed partial class EventManager
    {
        public Action DeliverReceiptSuccess;
        public Action DeliverReceiptFailed;
        public Action CountdownPopup;
        public Action StoveWarning;
    }
}
