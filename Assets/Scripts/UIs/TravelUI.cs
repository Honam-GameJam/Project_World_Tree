using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TravelUI : PhaseUI
{
    [SerializeField] private List<PlayerProfile> _profiles;

    [SerializeField] private List<Button> _options;

    [SerializeField] private TextMeshProUGUI _timer;

    private int _curMember;
    private float _time;
    private bool _isTimeOver;

    private void Awake()
    {
        for (int i = 0; i < _options.Count; i++) {
            int index = i;
            _options[index].onClick.AddListener(() => SelectOption(index));
        }

        Init(GameManager.Instance.config.DefaultTravelTime);
    }

    public void Init(float time)
    {
        _curMember = 1;
        foreach (var profile in _profiles)
        {
            profile.gameObject.SetActive(false);
        }

        var player = GameManager.Instance.Player;

        foreach (var p in GameManager.Instance.Players)
        {
            if (p.ActorNumber == player.ActorNumber) continue;

            if (p.AreaIndex == player.AreaIndex)
            {
                _profiles[_curMember - 1].UpdateIcon(p.Icon);
                _profiles[_curMember - 1].UpdateName(p.Name);
                _curMember++;
            }
        }

        _time = time;
        _isTimeOver = false;
    }

    private void Update()
    {
        if (_isTimeOver) return;

        if (_time < 0f)
        {
            _isTimeOver = true;
            SelectOption(0);
        }

        _time -= Time.deltaTime;
        _timer.text = Mathf.CeilToInt(_time).ToString();
    }

    public void SelectOption(int index)
    {
        GameManager.Instance.SelectOption(index);
        GameManager.Instance.SetPhase(Game.Enum.Phase.GoHome);
    }
}