﻿using System.Collections;
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
        ManageStatus();
    }

    public void ManageStatus()
    {
        switch (status)
        {
            case 2:
                campfireAnimator.SetInteger("status", status);
                break;
            case 3:
                GetComponent<AudioSource>().mute = false;
                campfireAnimator.SetInteger("status", status);
                break;
        }
    }

    private void FixedUpdate()
    {
        if (!GetComponent<AudioSource>().mute)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            GetComponent<AudioSource>().spatialBlend = distanceToPlayer < 2 ? distanceToPlayer * 0.5f : 1;
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && DamageManager.healthAmount > 0)
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
                        GetComponent<AudioSource>().mute = false;
                        referenceObject.GetComponent<AudioPlayer>().PlaySound(9);
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
            status = 2;
            campfireAnimator.SetInteger("status", status);
            referenceObject.GetComponent<AudioPlayer>().PlaySound(5);
        }
    }
    //placeholder method
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
        TextAsset jsonData = InventoryManager.itemsJsonFile;
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
