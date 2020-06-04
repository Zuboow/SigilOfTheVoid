using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //working version
    public static bool isOpen = false;
    int inventorySize = 18, slotsInRow = 3;
    public GameObject inventorySlotPrefab;

    public List<Sprite> itemSprites = new List<Sprite>();
    public static List<Item> itemsInInventory = new List<Item>();
    public List<AudioClip> inventorySoundEffects = new List<AudioClip>();

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

        if (Input.GetKeyDown(KeyCode.E) && GetComponent<DamageManager>().healthAmount > 0)
        {
            PlaySound(4);
            AddItem("CookedMeatIcon", "Cooked Meat", "Has lots of proteins. Very popular in Gaul.", 7, 1, true, 35);
        }
        if (Input.GetKeyDown(KeyCode.R) && GetComponent<DamageManager>().healthAmount > 0)
        {
            PlaySound(4);
            AddItem("RawMeatIcon", "Raw Meat", "I'm pretty sure I should cook it first...", 3, 1, true, -15);
        }
    }

    void OpenInventory()
    {
        PlaySound(2);
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
                itemInInventorySpawner.GetComponent<SpriteRenderer>().sprite = itemsInInventory[a - 1]._icon;
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
        PlaySound(3);

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
                itemInInventorySpawner.GetComponent<SpriteRenderer>().sprite = itemsInInventory[a - 1]._icon;
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

    void AddItem(string spriteName, string name, string description, int value, int quantity, bool healingItem, int healing)
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
            foreach (Sprite s in itemSprites)
            {
                if (s.name == spriteName)
                {
                    itemsInInventory[freeSlotID] = (new Item(name, description, value, quantity, s, freeSlotID, healingItem, healing));
                    Debug.Log(""+ name +" added to inventory.");
                    if (isOpen) ReloadInventory();
                    break;
                }
            }
        }
        else
        {
            Debug.Log("Inventory full.");
        }
    }

    public void DeleteItem(int index)
    {
        itemsInInventory[index] = null;
        if (isOpen) ReloadInventory();
    }

    public void PlaySound(int type)
    {
        switch (type)
        {
            case 1:
                GetComponent<AudioSource>().clip = inventorySoundEffects[0];
                GetComponent<AudioSource>().Play();
                break;
            case 2:
                GetComponent<AudioSource>().clip = inventorySoundEffects[1];
                GetComponent<AudioSource>().Play();
                break;
            case 3:
                GetComponent<AudioSource>().clip = inventorySoundEffects[2];
                GetComponent<AudioSource>().Play();
                break;
            case 4:
                GetComponent<AudioSource>().clip = inventorySoundEffects[3];
                GetComponent<AudioSource>().Play();
                break;
        }
    }
}
