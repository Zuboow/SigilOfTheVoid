using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public GameObject referenceObject;
    GameObject hoveredItem, descriptionBackground;

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

    void OnMouseOver()
    {
        if (!ItemReplacer.itemDragged)
        {
            if (InventoryManager.isOpen)
            {
                int clickedSlotID = Int32.Parse(name.Split('_')[1]);
                if (InventoryManager.itemsInInventory[clickedSlotID - 1] != null)
                {
                    ShowDescription(InventoryManager.itemsInInventory[clickedSlotID - 1].name, InventoryManager.itemsInInventory[clickedSlotID - 1].description, InventoryManager.itemsInInventory[clickedSlotID - 1].value, InventoryManager.itemsInInventory[clickedSlotID - 1].usableItem);
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
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                int clickedSlotID = Int32.Parse(name.Split('_')[1]);

                Item draggedItem = ItemReplacer.draggedItemInstance;
                referenceObject.GetComponent<InventoryManager>().AddItem(draggedItem.spriteName, draggedItem.icon, draggedItem.name, draggedItem.description, draggedItem.value, 1, draggedItem.usableItem, draggedItem.healing, clickedSlotID - 1);
            }
        }
    }

    void ShowDescription(string itemName, string description, int value, bool usable)
    {
        if (hoveredItem == null)
        {
            GameObject descriptionSpawner = new GameObject();
            descriptionSpawner.AddComponent<TextMesh>();
            descriptionSpawner.GetComponent<TextMesh>().text = string.Format("<b>{0}</b> \n{1} \n" + (SettingsManager.language == "polish" ? "Wartosc" : "Value") + ": {2}G" + (usable == true ?
                "\n\n<color='green'>(E)</color> " + (SettingsManager.language == "polish" ? "Uzyj" : "Use") : ""), itemName, description, value);
            descriptionSpawner.GetComponent<TextMesh>().fontSize = 85;
            descriptionSpawner.GetComponent<TextMesh>().characterSize = 0.005f;
            descriptionSpawner.GetComponent<TextMesh>().alignment = TextAlignment.Left;
            descriptionSpawner.GetComponent<TextMesh>().anchor = TextAnchor.UpperLeft;
            GameObject descriptionText = Instantiate(descriptionSpawner, new Vector3(transform.position.x, transform.position.y - 0.1f, 10f), Quaternion.identity);
            Destroy(descriptionSpawner);
            descriptionSpawner = new GameObject();
            descriptionSpawner.AddComponent<SpriteRenderer>();
            descriptionSpawner.GetComponent<SpriteRenderer>().sprite = referenceObject.GetComponent<InventoryManager>().descriptionBackgroundTexture;
            GameObject descriptionBackgroundItem = Instantiate(descriptionSpawner, new Vector3(descriptionText.transform.position.x - 0.02f, descriptionText.transform.position.y + 0.01f, 10f), Quaternion.identity);
            Destroy(descriptionSpawner);
            descriptionText.GetComponent<MeshRenderer>().sortingLayerName = "UI";
            descriptionText.GetComponent<MeshRenderer>().sortingOrder = 18;
            descriptionText.GetComponent<TextMesh>().font = referenceObject.GetComponent<InventoryManager>().pixelatedFont;
            descriptionText.GetComponent<MeshRenderer>().material = referenceObject.GetComponent<InventoryManager>().pixelatedFont.material;

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
    }
}
