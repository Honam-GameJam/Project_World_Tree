using System;
using UnityEngine;

[Serializable]
public class Item
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int AreaIndex { get; private set; }
    [field: SerializeField] public int Value { get; private set; }
    [field: SerializeField] public int Weight { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}