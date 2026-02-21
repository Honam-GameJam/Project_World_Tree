using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfile : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _money;

    public void UpdateIcon(Sprite icon) => _icon.sprite = icon;
    public void UpdateName(string name) => _name.text = name;
    public void UpdateMoney(int money) => _money.text = money.ToString();
}