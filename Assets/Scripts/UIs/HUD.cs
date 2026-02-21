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
    [SerializeField] private List<Image> _inventory;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _money;

    private void Awake()
    {
        _send.onClick.AddListener(SendChat);
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

    public void UpdateIcon(Sprite icon) => _icon.sprite = icon;
    public void UpdateInventory(int index, Sprite item) => _inventory[index].sprite = item;
    public void UpdateName(string name) => _name.text = name;
    public void UpdateMoney(int money) => _money.text = money.ToString();
}