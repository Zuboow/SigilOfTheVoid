using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
    GameObject referenceObject;
    GameObject player;

    void OnEnable()
    {
        referenceObject = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && DamageManager.healthAmount > 0 
            && !ItemReplacer.itemDragged && Vector2.Distance(player.transform.position, transform.position) < 0.55)
        {
            TextAsset jsonData = InventoryManager.itemsJsonFile;
            Items values = JsonUtility.FromJson<Items>(jsonData.text);
            foreach (Item i in values.items)
            {
                if (name.Split('(')[0].Trim() == i.spriteName)
                {
                    if (referenceObject.GetComponent<InventoryManager>().AddItem(
                        i.spriteName, this.GetComponent<SpriteRenderer>().sprite, i.name, i.description, i.value, 1, i.usableItem, i.healing, -1) == true)
                    {
                        referenceObject.GetComponent<AudioPlayer>().PlaySound(4);
                        Destroy(gameObject);
                    }
                    break;
                }
            }
        }
    }
}
