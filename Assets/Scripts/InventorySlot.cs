using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public GameObject referenceObject;
    GameObject hoveredItem, descriptionBackground;

    // Start is called before the first frame update
    void OnEnable()
    {
        referenceObject = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnMouseExit()
    {
        Destroy(hoveredItem);
        Destroy(descriptionBackground);
        hoveredItem = null;
        descriptionBackground = null;
    }

    private void OnDestroy()
    {
        if (hoveredItem != null)
        {
            Destroy(hoveredItem);
            Destroy(descriptionBackground);
            hoveredItem = null;
            descriptionBackground = null;
        }
    }

    // Update is called once per frame
    void OnMouseOver()
    {
        if (!ItemReplacer.itemDragged)
        {
            if (InventoryManager.isOpen)
            {
                int clickedSlotID = Int32.Parse(name.Split('_')[1]);
                if (InventoryManager.itemsInInventory[clickedSlotID - 1] != null)
                {
                    ShowDescription(InventoryManager.itemsInInventory[clickedSlotID - 1].name, InventoryManager.itemsInInventory[clickedSlotID - 1].description, InventoryManager.itemsInInventory[clickedSlotID - 1].value);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                DragItem();
            }
            if (Input.GetMouseButtonDown(1))
            {
                DropItem();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                UseItem();
            }
        } else
        {
            if (Input.GetMouseButtonDown(0))
            {
                int clickedSlotID = Int32.Parse(name.Split('_')[1]);
                if (InventoryManager.itemsInInventory[clickedSlotID - 1] == null)
                {
                    Item draggedItem = ItemReplacer.draggedItemInstance;
                    referenceObject.GetComponent<InventoryManager>().AddItem(draggedItem.spriteName, draggedItem.icon, draggedItem.name, draggedItem.description, draggedItem.value, 1, draggedItem.usableItem, draggedItem.healing, clickedSlotID - 1);
                }
            }
        }
    }

    void ShowDescription(string itemName, string description, int value)
    {
        if (hoveredItem == null)
        {
            GameObject descriptionSpawner = new GameObject();
            descriptionSpawner.AddComponent<TextMesh>();
            descriptionSpawner.GetComponent<TextMesh>().text = string.Format("<b>{0}</b> \n{1} \nValue: {2}G", itemName, description, value);
            descriptionSpawner.GetComponent<TextMesh>().fontSize = 80;
            descriptionSpawner.GetComponent<TextMesh>().characterSize = 0.005f;
            descriptionSpawner.GetComponent<TextMesh>().alignment = TextAlignment.Left;
            descriptionSpawner.GetComponent<TextMesh>().anchor = TextAnchor.LowerLeft;
            GameObject descriptionText = Instantiate(descriptionSpawner, new Vector3(transform.position.x, transform.position.y - 0.1f, 10f), Quaternion.identity);
            Destroy(descriptionSpawner);
            descriptionSpawner = new GameObject();
            descriptionSpawner.AddComponent<SpriteRenderer>();
            descriptionSpawner.GetComponent<SpriteRenderer>().sprite = referenceObject.GetComponent<InventoryManager>().descriptionBackgroundTexture;
            GameObject descriptionBackgroundItem = Instantiate(descriptionSpawner, new Vector3(descriptionText.transform.position.x - 0.02f, descriptionText.transform.position.y + 0.18f, 10f), Quaternion.identity);
            Destroy(descriptionSpawner);
            descriptionText.GetComponent<MeshRenderer>().sortingLayerName = "UI";
            descriptionText.GetComponent<MeshRenderer>().sortingOrder = 18;
            descriptionText.transform.parent = referenceObject.transform;
            hoveredItem = descriptionText;

            descriptionBackgroundItem.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
            descriptionBackgroundItem.GetComponent<SpriteRenderer>().sortingOrder = 17;
            descriptionBackgroundItem.transform.parent = referenceObject.transform;
            descriptionBackground = descriptionBackgroundItem;
        }
    }

    void UseItem()
    {
        int clickedSlotID = Int32.Parse(name.Split('_')[1]);
        if (InventoryManager.itemsInInventory[clickedSlotID - 1] != null)
        {
            if (InventoryManager.itemsInInventory[clickedSlotID - 1].usableItem)
            {
                Debug.Log(InventoryManager.itemsInInventory[clickedSlotID - 1].name + " eaten.");
                if (InventoryManager.itemsInInventory[clickedSlotID - 1].healing > 0)
                {
                    referenceObject.GetComponent<DamageManager>().Heal(InventoryManager.itemsInInventory[clickedSlotID - 1].healing);
                }
                else
                {
                    referenceObject.GetComponent<DamageManager>().DamagePlayer(InventoryManager.itemsInInventory[clickedSlotID - 1].healing);
                }
                referenceObject.GetComponent<AudioPlayer>().PlaySound(1);
                referenceObject.GetComponent<InventoryManager>().DeleteItem(clickedSlotID - 1);
                Destroy(hoveredItem);
                Destroy(descriptionBackground);
                hoveredItem = null;
                descriptionBackground = null;
            }
        }
        else
        {
            Debug.Log("Empty slot.");
        }
    }

    void DropItem()
    {
        int clickedSlotID = Int32.Parse(name.Split('_')[1]);
        if (InventoryManager.itemsInInventory[clickedSlotID - 1] != null)
        {
            referenceObject.GetComponent<InventoryManager>().DropItem(clickedSlotID - 1);
            Destroy(hoveredItem);
            Destroy(descriptionBackground);
            hoveredItem = null;
            descriptionBackground = null;
        }
        else
        {
            Debug.Log("Empty slot.");
        }
    }

    void DragItem()
    {
        int clickedSlotID = Int32.Parse(name.Split('_')[1]);
        if (InventoryManager.itemsInInventory[clickedSlotID - 1] != null)
        {
            ItemReplacer.DragItem(InventoryManager.itemsInInventory[clickedSlotID - 1], clickedSlotID - 1);

            referenceObject.GetComponent<InventoryManager>().DeleteItem(clickedSlotID - 1);
            Destroy(hoveredItem);
            Destroy(descriptionBackground);
            hoveredItem = null;
            descriptionBackground = null;
        }
        else
        {
            Debug.Log("Empty slot.");
        }
    }
}
