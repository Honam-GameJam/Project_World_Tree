using System;
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
    }

    public void OnEnable()
    {
        _time = GameManager.Instance.Config.DefaultTravelSelectionTime;
        _timer.text = Mathf.CeilToInt(_time).ToString();
        _isTimeOver = false;
        GameManager.Instance.ClickArea(_areas.Count-1);
    }

    private void Update()
    {
        if (_isTimeOver) return;

        if (_time < 0f)
        {
            _isTimeOver = true;
            GameManager.Instance.AsyncPhase();
        }

        _time -= Time.deltaTime;
        _timer.text = Mathf.CeilToInt(_time).ToString();
    }
}