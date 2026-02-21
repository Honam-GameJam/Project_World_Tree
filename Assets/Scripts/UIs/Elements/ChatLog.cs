using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatLog : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _text;

    public void Init(Sprite icon, string text)
    {
        _icon.sprite = icon;
        _text.text = text;
    }
}