using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TravelSelectionUI : PhaseUI
{
    [SerializeField] private List<Button> _areas;
    [SerializeField] private TextMeshProUGUI _timer;

    private float _time;
    private bool _isTimeOver;

    private void Awake()
    {
        for(int i = 0; i < _areas.Count; i++)
        {
            int index = i;
            _areas[i].onClick.AddListener(() => GameManager.Instance.ClickArea(index));
        }

        Init(GameManager.Instance.config.DefaultTravelSelectionTime);
    }

    public void Init(float time)
    {
        _time = time;
        _isTimeOver = false;
    }

    private void Update()
    {
        if (_isTimeOver) return;

        if (_time < 0f)
        {
            _isTimeOver = true;
            GameManager.Instance.SetPhase(Game.Enum.Phase.Travel);
        }

        _time -= Time.deltaTime;
        _timer.text = Mathf.CeilToInt(_time).ToString();
    }
}