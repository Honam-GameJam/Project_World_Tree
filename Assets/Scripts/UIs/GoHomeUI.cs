using System.Collections;
using UnityEngine;

public class GoHomeUI : PhaseUI
{
    private WaitForSeconds delay = new WaitForSeconds(1.5f);

    private void OnEnable()
    {
        StartCoroutine(nameof(NextPhase));
    }

    private IEnumerator NextPhase()
    {
        yield return delay;

        GameManager.Instance.AsyncPhase();
    }
}
