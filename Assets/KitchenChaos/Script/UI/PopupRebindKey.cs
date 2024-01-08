using UISystem;

public sealed class PopupRebindKey : BaseScreen
{
    public override void OnPop()
    {
        Destroy(gameObject);
    }
}
