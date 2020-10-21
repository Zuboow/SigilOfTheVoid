using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    public void DropItems(List<string> names, Vector3 g)
    {
        foreach (string n in names)
        {
            GameObject droppedItem = Instantiate(Resources.Load("Prefabs/Items/" + n) as GameObject, g, Quaternion.identity);
            droppedItem.name = n;
        }
    }
}
