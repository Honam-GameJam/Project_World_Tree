using Game.Enum;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool _canStartGame;
    public Phase Phase { get; private set; }

    private Dictionary<Phase, Action> _events = new();

    public void Ready()
    {

    }

    public void GameStart()
    {
        if (!_canStartGame) Debug.Log("게임을 시작할 수 없습니다.");

        SetPhase(Phase.TravelSelection);
    }

    public void SetPhase(Phase phase)
    {
        Phase = phase;

        if (_events.TryGetValue(phase, out Action entry))
        {
            entry.Invoke();
        }
    }

    public void AddListener(Phase phase, Action action)
    {
        if (_events.TryGetValue(phase, out Action entry))
        {
            entry += action;
        }
        else
        {
            entry = action;
            _events.Add(phase, entry);
        }
    }

    public void RemoveListener(Phase phase, Action action)
    {
        if (_events.TryGetValue(phase, out Action entry))
        {
            entry -= action;
        }
    }
}