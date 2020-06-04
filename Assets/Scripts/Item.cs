using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string _name, _description;
    public int _value, _quantity, _slotNumber, _healing;
    public bool _healingItem;
    public Sprite _icon;

    public Item(string name, string description, int value, int quantity, Sprite icon, int slotNumber, bool healingItem, int healing)
    {
        _name = name;
        _description = description;
        _value = value;
        _quantity = quantity;
        _icon = icon;
        _slotNumber = slotNumber;
        _healingItem = healingItem;
        _healing = healing;
    }
}
