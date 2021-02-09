using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSelector : MonoBehaviour
{
    public GameObject optionsSelector, mainSelector;
    void OnEnable()
    {
        if (name == "BackButton")
        {
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
