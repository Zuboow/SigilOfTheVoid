using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireInteractionManager : MonoBehaviour
{
    public Animator campfireAnimator;
    GameObject referenceObject, player;
    public int status = 1;

    void OnEnable()
    {
        referenceObject = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && referenceObject.GetComponent<DamageManager>().healthAmount > 0)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < 0.55)
            {
                switch (status)
                {
                    case 1:
                        RefillCampfire();
                        break;
                    case 2:
                        status = 3;
                        campfireAnimator.SetInteger("status", status);
                        break;
                    case 3:
                        CookMeat("RawMeat");
                        break;
                }
            }
        }
    }

    void RefillCampfire()
    {
        if (ManageCampfireItems(new List<string>() { "WoodenLog" }))
        {
            status = 2; // tests -> should be as '2'
            campfireAnimator.SetInteger("status", status);
            referenceObject.GetComponent<AudioPlayer>().PlaySound(5);
        }
    }
    //temporary shitstorm
    void CookMeat(string type)
    {
        if (ManageCampfireItems(new List<string>() { "RawMeat" }))
        {
            Debug.Log("Cooking...");
            ReceiveCraftingItem("CookedMeat");
        } else if (ManageCampfireItems(new List<string>() { "DeadMouse" }))
        {
            Debug.Log("Cooking...");
            ReceiveCraftingItem("CookedMouse");
        }
    }

    bool ManageCampfireItems(List<string> items)
    {
        List<int> itemsAvailable = referenceObject.GetComponent<InventoryManager>().CheckIfItemsAvailable(InventoryManager.itemsInInventory, items);
        if (itemsAvailable != null && itemsAvailable.Count > 0)
        {
            foreach (int i in itemsAvailable)
            {
                referenceObject.GetComponent<InventoryManager>().DeleteItem(i);
            }
            return true;
        }
        else
        {
            return false;
        }

    }

    string ReceiveCraftingItem(string itemName)
    {
        TextAsset jsonData = referenceObject.GetComponent<InventoryManager>().itemsJsonFile;
        Items values = JsonUtility.FromJson<Items>(jsonData.text);
        foreach (Item i in values.items)
        {
            if (itemName == i.spriteName)
            {
                referenceObject.GetComponent<AudioPlayer>().PlaySound(6);
                referenceObject.GetComponent<InventoryManager>().AddItem(
                    i.spriteName, SpriteLoader.LoadSprite(itemName), i.name, i.description, i.value, 1, i.usableItem, i.healing, -1);
                return i.spriteName;
            }
        }
        return null;
    }
}
