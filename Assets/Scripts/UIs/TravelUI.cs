using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TravelUI : PhaseUI
{
    [SerializeField] private List<PlayerProfile> _profiles;

    [SerializeField] private Button _basicOption;
    [SerializeField] private Button _betterOption;

    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private ConfigSO _config;

    private int _curMember;
    private float _time;
    private bool _isTimeOver;

    private void Awake()
    {
        _basicOption.onClick.AddListener(null);
        _betterOption.onClick.AddListener(null);

        Init(_config.DefaultTravelTime);
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
            GameManager.Instance.SetPhase(Game.Enum.Phase.Travel);
        }

        _time -= Time.deltaTime;
        _timer.text = Mathf.CeilToInt(_time).ToString();
    }
}