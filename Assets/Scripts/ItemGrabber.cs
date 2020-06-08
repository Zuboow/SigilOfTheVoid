using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
    GameObject referenceObject;

    // Start is called before the first frame update
    void OnEnable()
    {
        referenceObject = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && referenceObject.GetComponent<DamageManager>().healthAmount > 0)
        {
            TextAsset jsonData = referenceObject.GetComponent<InventoryManager>().itemsJsonFile;
            Items values = JsonUtility.FromJson<Items>(jsonData.text);
            foreach (Item i in values.items)
            {
                if (name == i.spriteName)
                {
                    referenceObject.GetComponent<AudioPlayer>().PlaySound(4);
                    referenceObject.GetComponent<InventoryManager>().AddItem(
                        i.spriteName, this.GetComponent<SpriteRenderer>().sprite, i.name, i.description, i.value, 1, i.usableItem, i.healing);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
