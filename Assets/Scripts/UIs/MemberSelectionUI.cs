using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MemberSelectionUI : PhaseUI
{
    [SerializeField] private List<PlayerInfo> _playerInfos;
    [SerializeField] private Button _confirmBtn;
    [SerializeField] private TextMeshProUGUI _count;

    //선택된 인원 리스트로 저장
    private int[] selectionArr;
    private int selectionCnt = 0;

    private void Awake()
    {
        var playerSize = GameManager.Instance.Config.MaxPlayer;

        int i = 0;
        foreach (var player in GameManager.Instance.Players)
        {
            _playerInfos[i].gameObject.SetActive(true);
            _playerInfos[i].UpdateIcon(player.Icon);
            _playerInfos[i].UpdateName(player.Name);
            _playerInfos[i].UpdateMoney(player.Money);

            i++;
        }

        for (i = 0; i < playerSize; i++)
        {
            int index = i;
            _playerInfos[i].Button.onClick.AddListener(() => SelectionArray(index));
        }

        _confirmBtn.onClick.AddListener(Confirm);
    }

    private void OnEnable()
    {
        selectionCnt = 0;
        selectionArr = new int[GameManager.Instance.Config.VotedPlayer];
        UpdateCount();

        if (!GameManager.Instance.Player.IsLeader) GameManager.Instance.AsyncPhase();
    }

    public void SelectionArray(int index)
    {
        //check
        for(int i = 0; i < selectionCnt; i++)
        {
            if (selectionArr[i] == index)
            {
                Debug.Log("Already Selected Player");
                return;
            }
        }

        if (selectionCnt < 4)
            selectionArr[selectionCnt++] = index;
        else
            Debug.Log("selection array full");

        UpdateCount();
    }

    private void Confirm()
    {
        GameManager.Instance.DeliverSelectionArray(selectionArr);
        GameManager.Instance.AsyncPhase();
    }

    private void UpdateCount() => _count.text = $"확정 ({selectionCnt}/{selectionArr.Length})";
}
