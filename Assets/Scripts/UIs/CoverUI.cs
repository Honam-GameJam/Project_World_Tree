using TMPro;
using UnityEngine;

public class CoverUI : UIBase
{
    [SerializeField] TextMeshProUGUI _waitingText;

    float timer;
    int dotCount;

    private void Awake()
    {
        timer = Time.time;
        dotCount = 1;
    }

    private void Update()
    {
        if (Time.time - timer > 0.3f)
        {
            timer = Time.time;

            dotCount++;
            if (dotCount % 3 == 0)
            {
                _waitingText.text = "다른 플레이어의 행동을 기다리고 있습니다.";
            }
            else if (dotCount % 3 == 1)
            {
                _waitingText.text = "다른 플레이어의 행동을 기다리고 있습니다..";
            }
            else if (dotCount % 3 == 2)
            {
                _waitingText.text = "다른 플레이어의 행동을 기다리고 있습니다...";
            }
        }
    }
}