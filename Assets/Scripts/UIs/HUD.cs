using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : UIBase
{
    [Header("Chat")]
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _send;
    [SerializeField] private Transform _chatParent;
    [SerializeField] private ChatLog _chatPrefab_mine;
    [SerializeField] private ChatLog _chatPrefab_other;

    [Header("Status")]
    [SerializeField] private Image _icon;
    [SerializeField] private List<ItemSlot> _inventory;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _money;

    [Header("Round")]
    [SerializeField] private TextMeshProUGUI _round;

    private void Awake()
    {
        _send.onClick.AddListener(SendChat);

        for (int i = 0; i < _inventory.Count; i++)
        {
            var item = _inventory[i];
            item.Init(i, true);
        }
    }

    public void Init()
    {
        UpdateIcon();
        UpdateName();
        UpdateMoney();
        UpdateRound();
    }

    private void SendChat()
    {
        if (string.IsNullOrWhiteSpace(_inputField.text)) return;

        GameManager.Instance.SendChat(_inputField.text);
        _inputField.text = "";
    }

    public void ReceiveChat(Sprite speaker, bool isMine, string chat)
    {
        var chatLog = isMine ? Instantiate(_chatPrefab_mine, _chatParent) : Instantiate(_chatPrefab_other, _chatParent);
        chatLog.Init(speaker, chat);
    }

    public void UpdateIcon() => _icon.sprite = GameManager.Instance.Player.Icon;
    public void UpdateName() => _name.text = GameManager.Instance.Player.Name;
    public void UpdateMoney() => _money.text = GameManager.Instance.Player.Money.ToString();
    public void UpdateRound() => _round.text = $"게임 라운드 [{GameManager.Instance.Round}/{GameManager.Instance.Config.Round}]";
    public void HideRound() => _round.gameObject.SetActive(false);
    public void ShowRound() => _round.gameObject.SetActive(true);
    public void UpdateInventory()
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            _inventory[i].UpdateItem(GameManager.Instance.Player.Inventory[i]);
        }
    }
    public void SetInventoryInteractable(bool isInteractable)
    {
        foreach (var item in _inventory)
        {
            item.SetInteractable(isInteractable);
        }
    }
}