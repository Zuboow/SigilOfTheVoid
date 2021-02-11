using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class GameSaver : MonoBehaviour
{
    public GameObject loadButton;
    public GameObject[] gameObjects, uniqueObjects, questGivers, destroyableObjects, objectsWithStatuses;
    public static Dictionary<string, Vector3> gameObjectTransforms = new Dictionary<string, Vector3>();
    public static Dictionary<string, GameObject> uniqueObjectsList = new Dictionary<string, GameObject>();
    public static Dictionary<string, int> objectsWithStatusesList = new Dictionary<string, int>();
    public static Dictionary<string, GameObject> destroyableObjectsList = new Dictionary<string, GameObject>();
    public static Dictionary<string, string[]> questGiverValues = new Dictionary<string, string[]>();

    private void OnEnable()
    {
        uniqueObjects = GameObject.FindGameObjectsWithTag("Unique");
        questGivers = GameObject.FindGameObjectsWithTag("QuestGiver");
        destroyableObjects = GameObject.FindGameObjectsWithTag("Destroyable");

        if (gameObjectTransforms.Count > 0)
        {
            LoadGameObjectTransform();
        }
        if (gameObjectTransforms.Count == 0)
        {
            loadButton.GetComponent<Text>().color = new Color32(108, 108, 108, 255);
        }
        else
        {
            loadButton.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        }
    }
    public void SaveGameObjectTransform()
    {
        uniqueObjectsList = new Dictionary<string, GameObject>();
        gameObjectTransforms = new Dictionary<string, Vector3>();
        destroyableObjectsList = new Dictionary<string, GameObject>();

        foreach(GameObject gObject in gameObjects)
        {
            if (gameObjectTransforms.ContainsKey(gObject.name))
            {
                gameObjectTransforms[gObject.name] = gObject.transform.position;
            } else
            {
                gameObjectTransforms.Add(gObject.name, gObject.transform.position);
            }
        }
        foreach (GameObject gObject in objectsWithStatuses)
        {
            if (objectsWithStatusesList.ContainsKey(gObject.name))
            {
                switch (gObject.name)
                {
                    case "Campfire":
                        objectsWithStatusesList[gObject.name] = gObject.GetComponent<CampfireInteractionManager>().status;
                        break;
                }
            }
            else
            {
                switch (gObject.name)
                {
                    case "Campfire":
                        objectsWithStatusesList.Add(gObject.name, gObject.GetComponent<CampfireInteractionManager>().status);
                        break;
                }
            }
        }
        for (int x = 0; x < destroyableObjects.Length; x++)
        {
            if (destroyableObjects[x] != null)
            {
                if (destroyableObjectsList.ContainsKey(destroyableObjects[x].name))
                {
                    destroyableObjectsList[destroyableObjects[x].name] = destroyableObjects[x];
                }
                else
                {
                    destroyableObjectsList.Add(destroyableObjects[x].name, destroyableObjects[x]);
                }
            }
        }
        foreach (GameObject qGiver in questGivers)
        {
            if (questGiverValues.ContainsKey(qGiver.name))
            {
                string[] a =
                {
                    qGiver.GetComponent<Quest_Manager>().questName,
                    qGiver.GetComponent<Quest_Manager>().questAvailable == true ? "true" : "false",
                    qGiver.GetComponent<Quest_Manager>().noRequirementQuestStarted == true ? "true" : "false",
                };
                questGiverValues[qGiver.name] = a;
            }
            else
            {
                string[] a =
                {
                    qGiver.GetComponent<Quest_Manager>().questName,
                    qGiver.GetComponent<Quest_Manager>().questAvailable == true ? "true" : "false",
                    qGiver.GetComponent<Quest_Manager>().noRequirementQuestStarted == true ? "true" : "false",
                };
                questGiverValues.Add(qGiver.name, a);
            }
        }
        for (int x = 0; x < uniqueObjects.Length; x++)
        {
            if (uniqueObjects[x] != null)
            {
                if (uniqueObjectsList.ContainsKey(uniqueObjects[x].name))
                {
                    uniqueObjectsList[uniqueObjects[x].name] = uniqueObjects[x];
                }
                else
                {
                    uniqueObjectsList.Add(uniqueObjects[x].name, uniqueObjects[x]);
                }
            }
        }
        if (GameSaver.gameObjectTransforms.Count == 0)
        {
            loadButton.GetComponent<Text>().color = new Color32(108, 108, 108, 255);
        }
        else
        {
            loadButton.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        }

        List<Item> items = new List<Item>();
        items.AddRange(InventoryManager.itemsInInventory);
        InventoryManager.loadedItemsInInventory = items;
    }

    public void LoadGameObjectTransform()
    {
        foreach (GameObject gObject in gameObjects)
        {
            gObject.transform.position = gameObjectTransforms[gObject.name];
        }
        foreach (GameObject gObject in objectsWithStatuses)
        {
            if (gObject.name == "Campfire")
            {
                gObject.GetComponent<CampfireInteractionManager>().status = objectsWithStatusesList[gObject.name];
                gObject.GetComponent<CampfireInteractionManager>().ManageStatus();
            }
        }
        foreach (GameObject g in uniqueObjects)
        {
            if (!uniqueObjectsList.ContainsKey(g.name))
            {
                Destroy(g);
            }
        }
        foreach (GameObject g in destroyableObjects)
        {
            if (!destroyableObjectsList.ContainsKey(g.name))
            {
                Destroy(g);
            }
        }
        foreach (GameObject g in questGivers)
        {
            g.GetComponent<Quest_Manager>().questName = questGiverValues[g.name][0];
            g.GetComponent<Quest_Manager>().questAvailable = (questGiverValues[g.name][1] == "true" ? true : false);
            g.GetComponent<Quest_Manager>().noRequirementQuestStarted = (questGiverValues[g.name][2] == "true" ? true : false);
        }
    }

}
