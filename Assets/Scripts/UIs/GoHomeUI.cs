public class GoHomeUI : PhaseUI
{
    private void OnEnable()
    {
        GameManager.Instance.AsyncPhase();
    }
}
