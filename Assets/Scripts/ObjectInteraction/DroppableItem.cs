using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DroppableItem
{
    public string name;
    public int percentageChance;

    public DroppableItem(string _name, int _percentageChance)
    {
        name = _name;
        percentageChance = _percentageChance;
    }
}
