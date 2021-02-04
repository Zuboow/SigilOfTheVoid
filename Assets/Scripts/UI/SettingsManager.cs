using DigitalRuby.SimpleLUT;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    GameObject resolutionDropdown;
    bool fullscreen = true;

    void OnEnable()
    {
        Screen.SetResolution(1920, 1080, fullscreen);
    }

    void FixedUpdate()
    {
        if (resolutionDropdown == null && GameObject.FindGameObjectWithTag("DropdownRes") != null)
        {
            resolutionDropdown = GameObject.FindGameObjectWithTag("DropdownRes").gameObject;
        }
    }

    public void SetFullscreen(bool isFs)
    {
        fullscreen = isFs;
        Screen.fullScreen = fullscreen;
    }

    public void SetResolution(int index)
    {
        List<UnityEngine.UI.Dropdown.OptionData> options = resolutionDropdown.GetComponent<UnityEngine.UI.Dropdown>().options;
        string[] selectedOption = options[index].text.ToString().Split('x');
        Screen.SetResolution(Int32.Parse(selectedOption[0]), Int32.Parse(selectedOption[1]), fullscreen);
    }

    public void SetBrightness(float level)
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SimpleLUT>().Brightness = (level * 0.5f);
    }

    public void SetContrast(float level)
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SimpleLUT>().Contrast = (level * 0.5f);
    }
}
