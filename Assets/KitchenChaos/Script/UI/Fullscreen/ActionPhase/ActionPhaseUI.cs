using UISystem;

namespace KitchenChaos
{
    public sealed class ActionPhaseUI : BaseScreen
    {
        public override void OnPop()
        {
            Destroy(gameObject);
        }
    }
}
