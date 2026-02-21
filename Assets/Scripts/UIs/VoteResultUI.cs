using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteResultUI : PhaseUI
{

    [SerializeField] private List<PlayerProfile> _playerInfos;
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
