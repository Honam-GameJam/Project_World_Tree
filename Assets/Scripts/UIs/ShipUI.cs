using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipUI : PhaseUI
{
    [SerializeField] private List<ItemSlot> _inventory;
    [SerializeField] private Button _submit;

    private void Awake()
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            var item = _inventory[i];
            item.Init(i, false);
            item.SetInteractable(true);
        }

        _submit.onClick.AddListener(Submit);
    }

    private void Start()
    {
        if (!GameManager.Instance.Player.HasShipTicket) GameManager.Instance.AsyncPhase();
    }

    public void Submit()
    {
        GameManager.Instance.SubmitItem();
        GameManager.Instance.AsyncPhase();
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            _inventory[i].UpdateItem(GameManager.Instance.Player.Ship[i]);
        }
    }
}