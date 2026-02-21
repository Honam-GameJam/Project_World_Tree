using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private List<Item> _items;

    private Dictionary<int, AliasTable> _areaTables;

    private void Init()
    {
        _areaTables = new();

        var grouped = _items.GroupBy(i => i.AreaIndex);

        foreach (var group in grouped)
        {
            _areaTables[group.Key] = new AliasTable(group.ToList());
        }
    }

    public Item GetAreaItem(int areaIndex)
    {
        if (_areaTables == null) Init();

        if (!_areaTables.TryGetValue(areaIndex, out var table))
            return null;

        return table.Sample();
    }

    public Item GetItem(int id)
    {
        foreach (var item in _items)
        {
            if (item.ID == id) return item;
        }

        return null;
    }
}
