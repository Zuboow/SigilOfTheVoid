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
    public GameObject brightnessSlider, contrastSlider, fullscreenCheckbox, sensitivitySlider, yAxisInvertedCheckbox, polishLangOption, englishLangOption;
    bool fullscreen = true, yAxisInverted = false;
    float brightness = 0, contrast = 0, sensitivity = 0;
    string resolution = "1920x1080", language = "polish";

    void OnEnable()
    {
        if (File.Exists(Application.dataPath + "/Resources/config.txt"))
        {
            File.ReadAllText(Application.dataPath + "/Resources/config.txt");
            StreamReader tr = new StreamReader(Application.dataPath + "/Resources/config.txt", true);
            String[] settings = tr.ReadLine().Trim().Split('%');
            tr.Close();
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SimpleLUT>().Brightness = float.Parse(settings[0]);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SimpleLUT>().Contrast = float.Parse(settings[1]);
            brightnessSlider.GetComponent<UnityEngine.UI.Slider>().value = float.Parse(settings[0]) * 2f;
            contrastSlider.GetComponent<UnityEngine.UI.Slider>().value = float.Parse(settings[1]) * 2f;
            sensitivitySlider.GetComponent<UnityEngine.UI.Slider>().value = float.Parse(settings[4]);
            fullscreenCheckbox.GetComponent<UnityEngine.UI.Toggle>().isOn = settings[3] == "1" ? true : false;
            yAxisInvertedCheckbox.GetComponent<UnityEngine.UI.Toggle>().isOn = settings[5] == "1" ? true : false;

            resolution = settings[2];
            string[] selectedOption = settings[2].Split('x');
            Screen.SetResolution(Int32.Parse(selectedOption[0]), Int32.Parse(selectedOption[1]), (settings[3] == "1" ? true : false));

            switch (settings[6])
            {
                case "english":
                    englishLangOption.GetComponent<Text>().color = new Color32(108, 108, 108, 255);
                    polishLangOption.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                    break;
                case "polish":
                    polishLangOption.GetComponent<Text>().color = new Color32(108, 108, 108, 255);
                    englishLangOption.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                    break;
            }
        }


    }

    void FixedUpdate()
    {
        if (resolutionDropdown == null && GameObject.FindGameObjectWithTag("DropdownRes") != null)
        {
            resolutionDropdown = GameObject.FindGameObjectWithTag("DropdownRes").gameObject;

            List<UnityEngine.UI.Dropdown.OptionData> options = resolutionDropdown.GetComponent<UnityEngine.UI.Dropdown>().options;
            for (int x = 0; x < options.Count; x++)
            {
                if (options[x].text == resolution)
                {
                    resolutionDropdown.GetComponent<UnityEngine.UI.Dropdown>().value = x;
                }
            }
        }
    }

    public void SetFullscreen(bool isFs)
    {
        fullscreen = isFs;
        Screen.fullScreen = fullscreen;
        SaveConfigFile();
    }

    public void SetYAxisInvert(bool isInv)
    {
        yAxisInverted = isInv;
        SaveConfigFile();
    }

    public void SetResolution(int index)
    {
        List<UnityEngine.UI.Dropdown.OptionData> options = resolutionDropdown.GetComponent<UnityEngine.UI.Dropdown>().options;
        string[] selectedOption = options[index].text.ToString().Split('x');
        Screen.SetResolution(Int32.Parse(selectedOption[0]), Int32.Parse(selectedOption[1]), fullscreen);
        resolution = selectedOption[0] + "x" + selectedOption[1];
        SaveConfigFile();
    }

    public void SetBrightness(float level)
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SimpleLUT>().Brightness = (level * 0.5f);
        brightness = (level * 0.5f);
        SaveConfigFile();
    }

    public void SetContrast(float level)
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SimpleLUT>().Contrast = (level * 0.5f);
        contrast = (level * 0.5f);
        SaveConfigFile();
    }

    public void SetSensitivity(float level)
    {
        sensitivity = level;
        SaveConfigFile();
    }

    public void SetLanguage(string lang)
    {
        language = lang;
        SaveConfigFile();
    }

    public void SaveConfigFile()
    {
        File.WriteAllText(Application.dataPath + "/Resources/config.txt", String.Empty);
        TextWriter tw = new StreamWriter(Application.dataPath + "/Resources/config.txt", true);
        tw.WriteLine(brightness + "%" + contrast + "%" + resolution + "%" + (fullscreen ? 1 : 0) + "%" + sensitivity + "%" + (yAxisInverted ? 1 : 0) + "%" + language);
        tw.Close();
    }
}
