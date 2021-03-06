﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Manager : MonoBehaviour
{
    public string questName;
    public bool questAvailable, noRequirementQuestStarted = false;
    int questState;
    GameObject referenceObject, player, showedText;
    static Dictionary<string, int> killedEnemies = new Dictionary<string, int>();


    static Quests questsJson;
    static List<Quest> heroQuests = new List<Quest>();

    private void OnEnable()
    {
        referenceObject = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");

        LoadQuests(SettingsManager.language);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && DamageManager.healthAmount > 0
            && !ItemReplacer.itemDragged && Vector2.Distance(player.transform.position, transform.position) < 0.55)
        {
            switch (questState)
            {
                case 0:
                    StartQuest(questName);
                    break;
                case 1:
                    CheckIfCanBeFinished(InventoryManager.itemsInInventory, questName);
                    break;
            }
        }
    }

    public static void AddKilledEnemy(string enemyName)
    {
        if (!killedEnemies.ContainsKey(enemyName))
        {
            killedEnemies.Add(enemyName, 1);
        }
        else
        {
            killedEnemies[enemyName] = killedEnemies[enemyName] + 1;
        }
    }

    void StartQuest(string questName)
    {
        if (questAvailable)
        {
            foreach (Quest q in questsJson.quests)
            {
                if (questName == q.name)
                {
                    heroQuests.Add(q);
                    StopAllCoroutines();
                    StartCoroutine(ShowLine(q.questIntroductionLine));
                    questState = 1;
                }
            }
        }
        else
        {
            Debug.Log("Quest not available yet.");
        }
    }

    void CheckIfCanBeFinished(List<Item> inventory, string questName)
    {
        Quest activeQuest = null;
        List<Item> inventoryItems = new List<Item>();
        List<int> indexes = new List<int>();
        foreach (Item item in inventory)
        {
            inventoryItems.Add(item);
        }
        foreach (Quest q in heroQuests)
        {
            if (q.name == questName) activeQuest = q;
        }

        //
        if (activeQuest.noRequirement)
        {
            if (!noRequirementQuestStarted)
            {
                StopAllCoroutines();
                StartCoroutine(ShowLine(activeQuest.questNotFinishedLine));
                noRequirementQuestStarted = true;
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(ShowLine(activeQuest.questFinishedLine));
                if (activeQuest.nextQuest != null)
                {
                    this.questName = activeQuest.nextQuest;
                    questState = 0;
                }
                else
                {
                    questState = 2;
                }
                for (int z = 0; z < activeQuest.rewards.Length; z++)
                {
                    GetRewards(activeQuest.rewards[z]);
                }
                if (InventoryManager.isOpen) referenceObject.GetComponent<InventoryManager>().ReloadInventory();
                noRequirementQuestStarted = false;
                if (activeQuest.setAvailability != null)
                    SetQuestAvailability(activeQuest.setAvailability);
            }
        }
        else if (activeQuest.neededEnemies.Length == 0)
        {
            List<string> neededItems = new List<string>();
            foreach (string c in activeQuest.neededItems)
            {
                neededItems.Add(c);
            }
            for (int s = 0; s < activeQuest.neededItems.Length; s++)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    if (inventoryItems[i] != null && inventoryItems[i].spriteName == neededItems[s])
                    {
                        indexes.Add(i);
                        neededItems[s] = null;
                        inventoryItems[i] = null;
                        break;
                    }
                }
            }
            bool canBeFinished = true;
            foreach (string n in neededItems)
            {
                if (n != null)
                {
                    StopAllCoroutines();
                    StartCoroutine(ShowLine(activeQuest.questNotFinishedLine));
                    canBeFinished = false;
                    break;
                }
            }
            if (canBeFinished)
            {
                StopAllCoroutines();
                StartCoroutine(ShowLine(activeQuest.questFinishedLine));
                if (activeQuest.nextQuest != null)
                {
                    this.questName = activeQuest.nextQuest;
                    questState = 0;
                }
                else
                {
                    questState = 2;
                }
                for (int y = 0; y < indexes.Count; y++)
                {
                    referenceObject.GetComponent<InventoryManager>().DeleteItem(indexes[y]);
                }
                for (int z = 0; z < activeQuest.rewards.Length; z++)
                {
                    GetRewards(activeQuest.rewards[z]);
                }
                if (InventoryManager.isOpen) referenceObject.GetComponent<InventoryManager>().ReloadInventory();
                if (activeQuest.setAvailability != null)
                    SetQuestAvailability(activeQuest.setAvailability);
            }
        }
        else
        {
            if (!killedEnemies.ContainsKey(activeQuest.neededEnemies[0]) || killedEnemies[activeQuest.neededEnemies[0]] < activeQuest.amountOfNeededEnemies)
            {
                StopAllCoroutines();
                StartCoroutine(ShowLine(activeQuest.questNotFinishedLine));
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(ShowLine(activeQuest.questFinishedLine));
                if (activeQuest.nextQuest != null)
                {
                    this.questName = activeQuest.nextQuest;
                    questState = 0;
                }
                else
                {
                    questState = 2;
                }
                for (int z = 0; z < activeQuest.rewards.Length; z++)
                {
                    GetRewards(activeQuest.rewards[z]);
                }
                if (InventoryManager.isOpen) referenceObject.GetComponent<InventoryManager>().ReloadInventory();
                if (activeQuest.setAvailability != null)
                    SetQuestAvailability(activeQuest.setAvailability);
            }
        }
    }

    Item GetRewards(string itemName)
    {
        TextAsset jsonData = InventoryManager.itemsJsonFile;
        Items values = JsonUtility.FromJson<Items>(jsonData.text);
        foreach (Item i in values.items)
        {
            if (itemName == i.spriteName)
            {
                referenceObject.GetComponent<AudioPlayer>().PlaySound(4);
                bool inventoryEmpty = referenceObject.GetComponent<InventoryManager>().AddItem(
                    i.spriteName, SpriteLoader.LoadSprite(itemName), i.name, i.description, i.value, 1, i.usableItem, i.healing, -1);
                if (!inventoryEmpty)
                {
                    referenceObject.GetComponent<InventoryManager>().DropItem(itemName);
                }

                return i;
            }
        }
        return null;
    }

    void SetQuestAvailability(string questGiverName)
    {
        List<GameObject> questGivers = new List<GameObject>(GameObject.FindGameObjectsWithTag("QuestGiver"));
        foreach (GameObject questGiver in questGivers)
        {
            if (questGiverName == questGiver.name)
            {
                questGiver.GetComponent<Quest_Manager>().questAvailable = true;
            }
        }

    }

    public static void LoadQuests(string lang)
    {
        TextAsset jsonData = Resources.Load("JSON/" + lang + "Quests") as TextAsset;
        questsJson = JsonUtility.FromJson<Quests>(jsonData.text);

        foreach (Quest q in heroQuests)
        {
            Quests values = JsonUtility.FromJson<Quests>(jsonData.text);
            foreach (Quest i in values.quests)
            {
                if (i.name == q.name)
                {
                    q.questFinishedLine = i.questFinishedLine;
                    q.questIntroductionLine = i.questIntroductionLine;
                    q.questNotFinishedLine = i.questNotFinishedLine;
                }
            }
        }
    }

    IEnumerator ShowLine(string line)
    {
        if (showedText != null)
        {
            Destroy(showedText);
            showedText = null;
        }
        GameObject lineSpawner = new GameObject();
        lineSpawner.AddComponent<TextMesh>();
        lineSpawner.GetComponent<TextMesh>().text = string.Format(line);
        lineSpawner.GetComponent<TextMesh>().fontSize = 140;
        lineSpawner.GetComponent<TextMesh>().characterSize = 0.005f;
        lineSpawner.GetComponent<TextMesh>().alignment = TextAlignment.Center;
        lineSpawner.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
        showedText = Instantiate(lineSpawner, new Vector3(transform.position.x, transform.position.y + 0.05f, 10f), Quaternion.identity);
        Destroy(lineSpawner);
        showedText.GetComponent<MeshRenderer>().sortingLayerName = "UI";
        showedText.GetComponent<MeshRenderer>().sortingOrder = 12;
        Font pixelatedFont = Resources.Load("Fonts/alphbeta") as Font;
        showedText.GetComponent<TextMesh>().font = pixelatedFont;
        showedText.GetComponent<MeshRenderer>().material = pixelatedFont.material;

        yield return new WaitForSeconds(8f);
        Destroy(showedText);
        showedText = null;
        StopAllCoroutines();
    }
}
