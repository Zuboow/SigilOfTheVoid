using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string spriteName, name, description;
    public int value, quantity, healing;
    public bool usableItem;
    public Sprite icon;

    public Item(string _spriteName, string _name, string _description, int _value, int _quantity, Sprite _icon, bool _usableItem, int _healing)
    {
        spriteName = _spriteName;
        name = _name;
        description = _description;
        value = _value;
        quantity = _quantity;
        icon = _icon;
        usableItem = _usableItem;
        healing = _healing;
    }
}

[System.Serializable]
public class Items
{
    public Item[] items;
}
