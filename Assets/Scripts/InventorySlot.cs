using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public GameObject referenceObject;
    GameObject hoveredItem;

    // Start is called before the first frame update
    void OnEnable()
    {
        referenceObject = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnMouseExit()
    {
        if (hoveredItem != null)
        {
            Destroy(hoveredItem);
            hoveredItem = null;
        }
    }

    // Update is called once per frame
    void OnMouseOver()
    {
        if (InventoryManager.isOpen) {
            int clickedSlotID = Int32.Parse(name.Split('_')[1]);
            if (InventoryManager.itemsInInventory[clickedSlotID - 1] != null)
            {
                ShowDescription(InventoryManager.itemsInInventory[clickedSlotID - 1]._name, InventoryManager.itemsInInventory[clickedSlotID - 1]._description, InventoryManager.itemsInInventory[clickedSlotID - 1]._value);
            }
        }

        if (Input.GetMouseButtonDown(1)) // dropping object <- to do
        {
            int clickedSlotID = Int32.Parse(name.Split('_')[1]);
            if (InventoryManager.itemsInInventory[clickedSlotID - 1] != null)
            {
                Debug.Log(InventoryManager.itemsInInventory[clickedSlotID - 1]._name);
            } else
            {
                Debug.Log("Empty slot.");
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            int clickedSlotID = Int32.Parse(name.Split('_')[1]);
            if (InventoryManager.itemsInInventory[clickedSlotID - 1] != null)
            {
                if (InventoryManager.itemsInInventory[clickedSlotID - 1]._healingItem)
                {
                    Debug.Log(InventoryManager.itemsInInventory[clickedSlotID - 1]._name + " eaten.");
                    if (InventoryManager.itemsInInventory[clickedSlotID - 1]._healing > 0)
                    {
                        referenceObject.GetComponent<DamageManager>().Heal(InventoryManager.itemsInInventory[clickedSlotID - 1]._healing);
                    } else
                    {
                        referenceObject.GetComponent<DamageManager>().DamagePlayer(InventoryManager.itemsInInventory[clickedSlotID - 1]._healing);
                    }
                    referenceObject.GetComponent<InventoryManager>().PlaySound(1);
                    referenceObject.GetComponent<InventoryManager>().DeleteItem(clickedSlotID - 1);
                    Destroy(hoveredItem);
                    hoveredItem = null;
                }
            }
            else
            {
                Debug.Log("Empty slot.");
            }
        }
    }

    void ShowDescription(string itemName, string description, int value)
    {
        if (hoveredItem == null)
        {
            GameObject descriptionSpawner = new GameObject();
            descriptionSpawner.AddComponent<TextMesh>();
            descriptionSpawner.GetComponent<TextMesh>().text = string.Format("<b>{0}</b> \n{1} \nValue: {2}", itemName, description, value);
            descriptionSpawner.GetComponent<TextMesh>().fontSize = 80;
            descriptionSpawner.GetComponent<TextMesh>().characterSize = 0.005f;
            descriptionSpawner.GetComponent<TextMesh>().alignment = TextAlignment.Left;
            descriptionSpawner.GetComponent<TextMesh>().anchor = TextAnchor.LowerLeft;
            GameObject descriptionText = Instantiate(descriptionSpawner, new Vector3(transform.position.x, transform.position.y - 0.1f, 10f), Quaternion.identity);
            Destroy(descriptionSpawner);
            descriptionText.GetComponent<MeshRenderer>().sortingLayerName = "UI";
            descriptionText.GetComponent<MeshRenderer>().sortingOrder = 17;
            descriptionText.transform.parent = referenceObject.transform;
            hoveredItem = descriptionText;
        }
    }
}
