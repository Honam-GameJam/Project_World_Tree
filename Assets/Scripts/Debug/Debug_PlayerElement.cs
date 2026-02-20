using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Debug_PlayerElement : MonoBehaviour
{
    public Image Icon;
    public TextMeshProUGUI IDText;
    public int ID;

    public void Init(int spriteId, int id)
    {
        ID = id;
        IDText.text = id.ToString();
    }
}