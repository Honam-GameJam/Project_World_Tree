using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _icon;

    private int _itemId;
    private int _slotIndex;

    private bool _isEmpty;
    private bool _isClickable;
    private bool _isInventory;

    public void Init(int slotIndex, bool isInventory)
    {
        _slotIndex = slotIndex;
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

        if (itemId == -1)
        {
            _isEmpty = true;
            _icon.gameObject.SetActive(false);
        }
        else
        {
            var item = GameManager.Instance.Items.GetItem(itemId);
            _isEmpty = false;
            _icon.sprite = item.Sprite;
            _icon.gameObject.SetActive(true);
        }
    }

    public void SetInteractable(bool isInteractable)
    {
        _isClickable = isInteractable;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isEmpty || _itemId == -1) return;

        var item = GameManager.Instance.Items.GetItem(_itemId);
        TooltipSystem.Instance.Show(item.Name, item.Description, item.Value);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Instance.Hide();
    }
}
