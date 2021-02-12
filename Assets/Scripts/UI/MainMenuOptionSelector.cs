using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuOptionSelector : MonoBehaviour
{
    public GameObject optionsSelector, mainSelector, loader;

    void OnEnable()
    {
        if (name == "settings")
        {
            optionsSelector.SetActive(false);
        }
        if (name == "load")
        {
            if (File.Exists(Application.dataPath + "/Resources/save.txt"))
            loader.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            else
            loader.GetComponent<Text>().color = new Color32(108, 108, 108, 255);
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (name)
            {
                case "newGame":
                    GameSaver.gameObjectTransforms = new Dictionary<string, Vector3>();
                    InventoryManager.loadedItemsInInventory = new List<Item>();
                    SceneManager.LoadScene("Game");
                    break;
                case "load":
                    if (File.Exists(Application.dataPath + "/Resources/save.txt"))
                    {
                        GameSaver.load = true;
                        SceneManager.LoadScene("Game");
                    }
                    break;
                case "save":
                    loader.GetComponent<GameSaver>().SaveGameObjectTransform();
                    break;
                case "settings":
                    optionsSelector.SetActive(true);
                    break;
                case "exit":
                    Application.Quit();
                    break;
            }
        }
    }
}
