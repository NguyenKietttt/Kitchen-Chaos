using UISystem;

namespace KitchenChaos
{
    public sealed class RebindKeyUI : BaseScreen
    {
        public override void OnPop()
        {
            Destroy(gameObject);
        }
    }
}
