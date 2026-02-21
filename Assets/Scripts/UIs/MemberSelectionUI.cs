using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MemberSelectionUI : PhaseUI
{
    [SerializeField] private List<Button> _playerInfos;
    [SerializeField] private Button _confirmBtn;
    [SerializeField] private TextMeshProUGUI _count;

    //선택된 인원 리스트로 저장
    private int[] selectionArr = new int[GameManager.Instance.Config.MaxPlayer];
    private int selectionCnt = 0;

    private void Awake()
    {
        for (int i = 0; i < _playerInfos.Count; i++)
        {
            int index = i;
            _playerInfos[i].onClick.AddListener(() => SelectionArray(index));
        }

        _confirmBtn.onClick.AddListener(Confirm);
        UpdateCount();
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
    }

    private void UpdateCount() => _count.text = $"확정 ({selectionCnt}/{selectionArr.Length})";
}
