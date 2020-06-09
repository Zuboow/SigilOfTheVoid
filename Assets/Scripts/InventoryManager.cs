using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static bool isOpen = false;
    int inventorySize = 18, slotsInRow = 3;
    public GameObject inventorySlotPrefab;
    public Sprite descriptionBackgroundTexture;
    public TextAsset itemsJsonFile;
    public Font pixelatedFont;

    public static List<Item> itemsInInventory = new List<Item>();

    List<GameObject> inventorySlots = new List<GameObject>();
    List<GameObject> itemSpritesFromInventory = new List<GameObject>();
    float slotOffset = 0f, rowOffset = 0f;

    // Start is called before the first frame update
    void OnEnable()
    {
        for (int x = 0; x < inventorySize; x++)
        {
            itemsInInventory.Add(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isOpen && GetComponent<DamageManager>().healthAmount > 0)
        {
            OpenInventory();
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            CloseInventory();
        }
    }

    void OpenInventory()
    {
        GetComponent<AudioPlayer>().PlaySound(2);
        slotOffset = 0f;
        rowOffset = 0f;
        for (int a = 1; a < inventorySize + 1; a++)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, new Vector3(transform.localPosition.x - 1.55f + slotOffset, transform.localPosition.y + 0.4f - rowOffset, 15f), Quaternion.identity);
            slot.GetComponent<SpriteRenderer>().transform.parent = transform;
            slot.name = "inv_" + a;
            if (itemsInInventory[a - 1] != null)
            {
                GameObject itemInInventorySpawner = new GameObject();
                itemInInventorySpawner.AddComponent<SpriteRenderer>();
                itemInInventorySpawner.GetComponent<SpriteRenderer>().sprite = itemsInInventory[a - 1].icon;
                itemInInventorySpawner.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                itemInInventorySpawner.GetComponent<SpriteRenderer>().sortingOrder = 16;
                GameObject itemSpriteInSlot = Instantiate(itemInInventorySpawner, new Vector3(transform.localPosition.x - 1.55f + slotOffset, transform.localPosition.y + 0.4f - rowOffset, 16f), Quaternion.identity);
                itemSpriteInSlot.transform.parent = transform;
                Destroy(itemInInventorySpawner);
                itemSpritesFromInventory.Add(itemSpriteInSlot);
            }
            inventorySlots.Add(slot);

            if (a % slotsInRow == 0) { rowOffset += 0.24f; slotOffset = 0f; } else slotOffset += 0.24f;
        }
        isOpen = true;
    }

    public void CloseInventory()
    {
        GetComponent<AudioPlayer>().PlaySound(3);

        for (int x = 0; x < inventorySlots.Count; x++)
        {
            Destroy(inventorySlots[x]);
        }
        inventorySlots.Clear();
        for (int y = 0; y < itemSpritesFromInventory.Count; y++)
        {
            Destroy(itemSpritesFromInventory[y]);
        }
        itemSpritesFromInventory.Clear();
        isOpen = false;
    }

    public void ReloadInventory()
    {
        slotOffset = 0f;
        rowOffset = 0f;
        for (int y = 0; y < itemSpritesFromInventory.Count; y++)
        {
            Destroy(itemSpritesFromInventory[y]);
        }
        itemSpritesFromInventory.Clear();

        for (int a = 1; a < inventorySize + 1; a++)
        {
            if (itemsInInventory[a - 1] != null)
            {
                GameObject itemInInventorySpawner = new GameObject();
                itemInInventorySpawner.AddComponent<SpriteRenderer>();
                itemInInventorySpawner.GetComponent<SpriteRenderer>().sprite = itemsInInventory[a - 1].icon;
                itemInInventorySpawner.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                itemInInventorySpawner.GetComponent<SpriteRenderer>().sortingOrder = 16;
                GameObject itemSpriteInSlot = Instantiate(itemInInventorySpawner, new Vector3(transform.localPosition.x - 1.55f + slotOffset, transform.localPosition.y + 0.4f - rowOffset, 16f), Quaternion.identity);
                itemSpriteInSlot.transform.parent = transform;
                Destroy(itemInInventorySpawner);
                itemSpritesFromInventory.Add(itemSpriteInSlot);
            }
            if (a % slotsInRow == 0) { rowOffset += 0.24f; slotOffset = 0f; } else slotOffset += 0.24f;
        }
    }

    public bool AddItem(string spriteName, Sprite sprite, string name, string description, int value, int quantity, bool usableItem, int healing, int slotNumber)
    {
        if (slotNumber == -1)
        {
            int freeSlotID = -1;
            for (int x = 0; x < itemsInInventory.Count; x++)
            {
                if (itemsInInventory[x] == null)
                {
                    freeSlotID = x;
                    break;
                }
            }
            if (freeSlotID != -1)
            {
                itemsInInventory[freeSlotID] = (new Item(spriteName, name, description, value, quantity, sprite, usableItem, healing));
                Debug.Log("" + name + " added to inventory.");
                if (isOpen) ReloadInventory();
                return true;
            }
            else
            {
                Debug.Log("Inventory full.");
                return false;
            }
        } else
        {
            if (itemsInInventory[slotNumber] == null)
            {
                itemsInInventory[slotNumber] = (new Item(spriteName, name, description, value, quantity, sprite, usableItem, healing));
                Debug.Log("" + name + " has changed its slot.");
                if (isOpen) ReloadInventory();
                Destroy(ItemReplacer.draggedItem);
                ItemReplacer.itemDragged = false;
                return true;
            } else
            {
                itemsInInventory[ItemReplacer.originalSlot] = itemsInInventory[slotNumber];
                itemsInInventory[slotNumber] = (new Item(spriteName, name, description, value, quantity, sprite, usableItem, healing));
                Debug.Log("" + name + " has changed its slot.");
                if (isOpen) ReloadInventory();
                Destroy(ItemReplacer.draggedItem);
                ItemReplacer.itemDragged = false;
                return true;
            }
        }
    }

    public void DeleteItem(int index)
    {
        itemsInInventory[index] = null;
        if (isOpen) ReloadInventory();
    }

    public void DropItem(int index)
    {
        GameObject droppedItem = Instantiate(Resources.Load("Prefabs/Items/" + itemsInInventory[index].spriteName) as GameObject, new Vector3(transform.position.x, transform.position.y - 0.16f, 10f), Quaternion.identity);
        droppedItem.name = itemsInInventory[index].spriteName;
        itemsInInventory[index] = null;
        if (isOpen) ReloadInventory();
    }

    
}
