using System.Collections;
using System.Collections.Generic;
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
                    if (GameSaver.gameObjectTransforms.Count > 0)
                    {
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
