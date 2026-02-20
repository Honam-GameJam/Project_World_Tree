using Game.Enum;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    Dictionary<Phase, PhaseUI> _phaseUI = new();
    PhaseUI _lastPhaseUI;

    private void Awake()
    {
        var uis = Resources.LoadAll<UIBase>("UIs/");

        foreach (var ui in uis)
        {
            if (ui is PhaseUI)
            {
                var phase = ui as PhaseUI;
                _phaseUI[phase.Phase] = phase;

                GameManager.Instance.AddListener(phase.Phase, () => ShowPhaseUI(phase.Phase));
            }
        }
    }

    public PhaseUI GetPhaseUI(Phase phase)
    {
        if (!_phaseUI.TryGetValue(phase, out PhaseUI ui))
        {
            Debug.LogError("UI를 찾을 수 없습니다.");
            return null;
        }

        return ui;
    }

    public void ShowPhaseUI(Phase phase)
    {
        if (!_phaseUI.TryGetValue(phase, out PhaseUI ui))
        {
            Debug.LogError("UI를 찾을 수 없습니다.");
        }

        if (_lastPhaseUI != null)
        {
            _lastPhaseUI.gameObject.SetActive(false);
        }

        if (!ui.gameObject.scene.IsValid())
        {
            ui = Instantiate(ui);
        }

        _lastPhaseUI = ui;

        ui.gameObject.SetActive(true);
    }
}