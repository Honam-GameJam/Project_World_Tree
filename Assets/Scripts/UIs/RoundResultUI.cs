public class RoundResultUI : PhaseUI
{
    public void OnEnable()
    {
        GameManager.Instance.CaculateMoney();
    }
}