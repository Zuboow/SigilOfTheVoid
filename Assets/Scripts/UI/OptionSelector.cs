using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSelector : MonoBehaviour
{
    GameObject optionsSelector, mainSelector;
    void OnEnable()
    {
        optionsSelector = GameObject.FindGameObjectWithTag("OptionsSelector");
        if (name == "BackButton")
        {
            mainSelector = GameObject.FindGameObjectWithTag("MainSelector");
            mainSelector.SetActive(false);
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (name == "BackButton")
            {
                mainSelector.SetActive(true);
                optionsSelector.SetActive(false);
            } else
            {
                optionsSelector.GetComponent<OptionsController>().SetActiveWindow(name);
            }
        }
    }
}
