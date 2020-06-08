using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReplacer : MonoBehaviour
{
    public static bool itemDragged = false;
    public static GameObject draggedItem;
    public static Item draggedItemInstance;

    private void FixedUpdate()
    {
        if (itemDragged)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            draggedItem.transform.position = new Vector3(mousePosition.x, mousePosition.y, 10f);
        }
    }

    public static void DragItem(Item item)
    {
        draggedItemInstance = item;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject draggedItemSpawner = new GameObject();
        draggedItemSpawner.AddComponent<SpriteRenderer>();
        draggedItemSpawner.GetComponent<SpriteRenderer>().sprite = item.icon;
        draggedItemSpawner.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
        draggedItemSpawner.GetComponent<SpriteRenderer>().sortingOrder = 20;

        draggedItem = Instantiate(draggedItemSpawner, mousePosition, Quaternion.identity);
        Destroy(draggedItemSpawner);

        itemDragged = true;
    }
}
