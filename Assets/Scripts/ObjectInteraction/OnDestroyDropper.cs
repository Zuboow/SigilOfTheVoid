using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyDropper : MonoBehaviour
{
    public List<DroppableItem> randomizedItemsWithPercentage;
    public List<string> defaultItems;
    public void DropAndDestroy(GameObject g)
    {
        List<string> droppedItems = new List<string>();
        droppedItems.AddRange(defaultItems);
        for(int x = 0; x < randomizedItemsWithPercentage.Count; x++)
        {
            int randomizedNumber = Random.Range(0, 100);
            if (randomizedNumber >= (100 - randomizedItemsWithPercentage[x].percentageChance))
                droppedItems.Add(randomizedItemsWithPercentage[x].name);
        }
        Vector3 gPosition = new Vector3(g.transform.position.x, g.transform.position.y, 10f);

        g.GetComponent<ItemDropper>().DropItems(droppedItems, gPosition);
        Destroy(g);
    }
}
