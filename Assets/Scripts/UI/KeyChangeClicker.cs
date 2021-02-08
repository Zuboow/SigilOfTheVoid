using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChangeClicker : MonoBehaviour
{
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (name == "defaultKeys") 
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SettingsManager>().SetDefaultKeys();
            }
            else
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SettingsManager>().EditKey(name);
            }
        }
    }
}
