using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuOptionSelector : MonoBehaviour
{
    public GameObject optionsSelector, mainSelector, loader, saver, loadButton;

    void OnEnable()
    {
        if (name == "settings")
        {
            optionsSelector.SetActive(false);
        }
        if (name == "save")
        {
            if ((File.Exists(Application.dataPath + "/Resources/save.txt") && DamageManager.healthAmount > 0) || !File.Exists(Application.dataPath + "/Resources/save.txt"))
                saver.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            else
                saver.GetComponent<Text>().color = new Color32(108, 108, 108, 255);
        }
        if (name == "load")
        {
            if (loadButton != null)
            {
                if (File.Exists(Application.dataPath + "/Resources/save.txt"))
                    loadButton.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                else
                    loadButton.GetComponent<Text>().color = new Color32(108, 108, 108, 255);
            }
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (name)
            {
                case "newGame":
                    GameSaver.load = false;
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
                    if (DamageManager.healthAmount > 0)
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
