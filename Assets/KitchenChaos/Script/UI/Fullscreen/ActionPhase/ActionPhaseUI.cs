using UISystem;

public sealed class ActionPhaseUI : BaseScreen
{
    public override void OnPop()
    {
        Destroy(gameObject);
    }
}
