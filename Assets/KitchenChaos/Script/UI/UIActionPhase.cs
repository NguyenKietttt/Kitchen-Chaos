using UISystem;

public sealed class UIActionPhase : BaseScreen
{
    public override void OnPop()
    {
        Destroy(gameObject);
    }
}
