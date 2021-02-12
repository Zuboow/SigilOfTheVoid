using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayingItemDescription : MonoBehaviour
{
    GameObject player, descriptionText, referenceObject;
    void OnEnable()
    {
        referenceObject = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnMouseOver()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < 0.55 && descriptionText == null && DamageManager.healthAmount > 0)
        {
            GameObject descriptionSpawner = new GameObject();
            descriptionSpawner.AddComponent<TextMesh>();
            descriptionSpawner.GetComponent<TextMesh>().text = string.Format(GetNameFromJSON());
            descriptionSpawner.GetComponent<TextMesh>().fontSize = 100;
            descriptionSpawner.GetComponent<TextMesh>().characterSize = 0.005f;
            descriptionSpawner.GetComponent<TextMesh>().alignment = TextAlignment.Center;
            descriptionSpawner.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
            descriptionText = Instantiate(descriptionSpawner, new Vector3(transform.position.x, transform.position.y + 0.05f, 10f), Quaternion.identity);
            Destroy(descriptionSpawner);
            descriptionText.GetComponent<MeshRenderer>().sortingLayerName = "UI";
            descriptionText.GetComponent<MeshRenderer>().sortingOrder = 12;
            Font pixelatedFont = Resources.Load("Fonts/alphbeta") as Font;
            descriptionText.GetComponent<TextMesh>().font = pixelatedFont;
            descriptionText.GetComponent<MeshRenderer>().material = pixelatedFont.material;
        }
        if ((Vector2.Distance(player.transform.position, transform.position) >= 0.55 && descriptionText != null) || DamageManager.healthAmount <= 0)
        {
            Destroy(descriptionText);
            descriptionText = null;
        }
    }

    private void OnMouseExit()
    {
        if (descriptionText != null)
        {
            Destroy(descriptionText);
            descriptionText = null;
        }
    }

    private void OnDestroy()
    {
        if (descriptionText != null)
        {
            Destroy(descriptionText);
            descriptionText = null;
        }
    }

    string GetNameFromJSON()
    {
        TextAsset jsonData = InventoryManager.itemsJsonFile;
        Items values = JsonUtility.FromJson<Items>(jsonData.text);
        foreach (Item i in values.items)
        {
            if (name.Split('(')[0].Trim() == i.spriteName)
            {
                return i.name;
            }
        }
        return name.Split('(')[0].Trim();
    }
}
