using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
    public static bool load = false;

    private void OnEnable()
    {
        uniqueObjects = GameObject.FindGameObjectsWithTag("Unique");
        questGivers = GameObject.FindGameObjectsWithTag("QuestGiver");
        destroyableObjects = GameObject.FindGameObjectsWithTag("Destroyable");

        if (File.Exists(Application.dataPath + "/Resources/save.txt"))
        {
            if (load)
            {
                LoadGameObjectTransform();
            }
            loadButton.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        } else
        {
            loadButton.GetComponent<Text>().color = new Color32(108, 108, 108, 255);
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
        //
        string gameObjectTransformsString = "";
        foreach (KeyValuePair<string, Vector3> pair in gameObjectTransforms)
        {
            gameObjectTransformsString += pair.Key + "#" + pair.Value + "^";
        }
        string objectsWithStatusesListString = "";
        foreach (KeyValuePair<string, int> pair in objectsWithStatusesList)
        {
            switch (pair.Key)
            {
                case "Campfire":
                    objectsWithStatusesListString += pair.Key + "#" + pair.Value + "^";
                    break;
            }
        }
        string uniqueObjectsListString = "";
        foreach (KeyValuePair<string, GameObject> pair in uniqueObjectsList)
        {
            uniqueObjectsListString += pair.Key + "^";
        }
        string destroyableObjectsListString = "";
        foreach (KeyValuePair<string, GameObject> pair in destroyableObjectsList)
        {
            destroyableObjectsListString += pair.Key + "^";
        }
        string questGiverValuesString = "";
        foreach (KeyValuePair<string, string[]> pair in questGiverValues)
        {
            questGiverValuesString += pair.Key + "#" + pair.Value[0] + "#" + pair.Value[1] + "#" + pair.Value[2] + "^";
        }
        string itemsString = "";
        foreach (Item i in InventoryManager.itemsInInventory)
        {
            itemsString += i != null ? i.spriteName + "^" : "EMPTY^";
        }
        File.WriteAllText(Application.dataPath + "/Resources/save.txt", String.Empty);
        TextWriter tw = new StreamWriter(Application.dataPath + "/Resources/save.txt", true);
        tw.WriteLine(gameObjectTransformsString + "%" + objectsWithStatusesListString + "%" + uniqueObjectsListString + "%" + destroyableObjectsListString + "%" + questGiverValuesString + "%" + 
            DamageManager.healthAmount + "%" + itemsString);
        tw.Close();
    }

    public void LoadGameObjectTransform()
    {
        File.ReadAllText(Application.dataPath + "/Resources/save.txt");
        StreamReader tr = new StreamReader(Application.dataPath + "/Resources/save.txt", true);
        string[] values = tr.ReadLine().Trim().Split('%');
        tr.Close();

        gameObjectTransforms = new Dictionary<string, Vector3>();
        objectsWithStatusesList = new Dictionary<string, int>();
        uniqueObjectsList = new Dictionary<string, GameObject>();
        destroyableObjectsList = new Dictionary<string, GameObject>();
        questGiverValues = new Dictionary<string, string[]>();

        //
        string[] parsedGameObjects = values[0].Split('^');
        foreach (string s in parsedGameObjects)
        {
            if (s.Length > 0)
            {
                string vectors = s.Split('#')[1];
                vectors = vectors.Remove(0, 1);
                vectors = vectors.Remove(vectors.Length - 1, 1);
                string[] vList = vectors.Split(',');

                gameObjectTransforms.Add(s.Split('#')[0], new Vector3(float.Parse(vList[0].Trim(), CultureInfo.InvariantCulture), float.Parse(vList[1].Trim(), CultureInfo.InvariantCulture), 
                    float.Parse(vList[2].Trim(), CultureInfo.InvariantCulture)));
            }
        }
        string[] parsedObjectsStatuses = values[1].Split('^');
        foreach (string s in parsedObjectsStatuses)
        {
            if (s.Length > 0)
            {
                objectsWithStatusesList.Add(s.Split('#')[0], Int32.Parse(s.Split('#')[1]));
            }
        }
        string[] parsedUniqueObjects = values[2].Split('^');
        foreach (string s in parsedUniqueObjects)
        {
            if (s.Length > 0)
            {
                uniqueObjectsList.Add(s, new GameObject());
            }
        }
        string[] parsedDestroyableObjects = values[3].Split('^');
        foreach (string s in parsedDestroyableObjects)
        {
            if (s.Length > 0)
            {
                destroyableObjectsList.Add(s, new GameObject());
            }
        }
        string[] parsedQuestGivers = values[4].Split('^');
        foreach (string s in parsedQuestGivers)
        {
            if (s.Length > 0)
            {
                string[] vList = s.Split('#');

                questGiverValues.Add(vList[0], new string[] { vList[1], vList[2], vList[3] });
            }
        }
        string[] parsedItems = values[6].Split('^');
        List<Item> loadedItemsInInventory = new List<Item>();
        foreach (string s in parsedItems)
        {
            if (s == "EMPTY")
            {
                loadedItemsInInventory.Add(null);
            } else
            {
                TextAsset jsonData = InventoryManager.itemsJsonFile;
                Items v = JsonUtility.FromJson<Items>(jsonData.text);
                foreach (Item i in v.items)
                {
                    if (s == i.spriteName)
                    {
                        loadedItemsInInventory.Add(new Item(i.spriteName, i.name, i.description, i.value, i.quantity, SpriteLoader.LoadSprite(i.spriteName), i.usableItem, i.healing));
                    }
                }
            }
        }
        //
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
        InventoryManager.itemsInInventory = loadedItemsInInventory;

        load = false;
    }

}
