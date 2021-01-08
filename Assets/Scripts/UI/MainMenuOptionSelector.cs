using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuOptionSelector : MonoBehaviour
{
    GameObject optionsSelector, mainSelector;

    void OnEnable()
    {
        mainSelector = GameObject.FindGameObjectWithTag("MainSelector");
        if (name == "Settings")
        {
            optionsSelector = GameObject.FindGameObjectWithTag("OptionsSelector");
            optionsSelector.SetActive(false);
        }
    }

    void Update()
    {
        
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (name)
            {
                case "NewGame":
                    SceneManager.LoadScene("Game");
                    break;
                case "Settings":
                    optionsSelector.SetActive(true);
                    break;
                case "Exit":
                    Application.Quit();
                    break;
            }
        }
    }
}
