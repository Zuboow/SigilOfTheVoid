using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyDropper : MonoBehaviour
{
    public bool DropAndDestroy(GameObject g)
    {
        Vector3 gPosition = new Vector3(g.transform.position.x, g.transform.position.y, 10f);
        string itemID = g.name;
        List<string> droppedItems = new List<string>() { "Apple", "CookedMeat" }; //na sztywno


        g.GetComponent<ItemDropper>().DropItems(droppedItems, gPosition);

        Destroy(g);
        return true;
    }
}
