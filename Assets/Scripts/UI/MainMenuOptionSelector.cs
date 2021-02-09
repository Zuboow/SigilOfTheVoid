using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuOptionSelector : MonoBehaviour
{
    public GameObject optionsSelector, mainSelector;

    void OnEnable()
    {
        if (name == "settings")
        {
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
                case "newGame":
                    SceneManager.LoadScene("Game");
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
