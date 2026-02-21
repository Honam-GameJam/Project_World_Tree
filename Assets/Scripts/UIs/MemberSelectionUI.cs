using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberSelectionUI : MonoBehaviour
{
    [SerializeField] private List<Button> _playerInfos;
    [SerializeField] private Button _confirmBtn;

    //선택된 인원 리스트로 저장
    private int[] selectionArr = new int[4]; //length 6
    private int selectionCnt = 0;

    private void Awake()
    {
        for (int i = 0; i < _playerInfos.Count; i++)
        {
            int index = i;
            _playerInfos[i].onClick.AddListener(() => SelectionArray(index));
        }

        _confirmBtn.onClick.AddListener(() => GameManager.Instance.DeliverSelectionArray(selectionArr));
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
    }
}
