using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _icon;

    private int _itemId;
    private int _slotIndex;

    private bool _isEmpty;
    private bool _isClickable;
    private bool _isInventory;

    public void Init(int _slotIndex, bool isInventory)
    {
        _isInventory = isInventory;
        _isEmpty = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isClickable || _isEmpty) return;

        _isEmpty = true;

        GameManager.Instance.ClickItem(_slotIndex, _isInventory);
    }

    public void UpdateItem(int itemId)
    {
        _itemId = itemId;
        if (itemId != -1) _isEmpty = false;

        _icon.gameObject.SetActive(!_isEmpty);
    }

    public void SetInteractable(bool isInteractable)
    {
        _isClickable = isInteractable;
    }
}
