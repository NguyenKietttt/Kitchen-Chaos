using UISystem;

public sealed class RebindKeyUI : BaseScreen
{
    public override void OnPop()
    {
        Destroy(gameObject);
    }
}
