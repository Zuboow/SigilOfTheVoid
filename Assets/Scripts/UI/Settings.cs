using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class Setting
{
    public string newGame, load, settings, exit, english, polish, contrast, brightness, resolution, fullscreen, soundS, controls, language,
        back, videoS, effects, music, speech, mouseS, controlsS, sensitivity, invertY, defaultKeys, attackS, equipmentS, pressAnyKey, save;

    public string this[string index]
    {
        get
        {
            switch (index)
            {
                case "newGame":
                    return newGame;
                case "load":
                    return load;
                case "settings":
                    return settings;
                case "exit":
                    return exit;
                case "english":
                    return english;
                case "polish":
                    return polish;
                case "contrast":
                    return contrast;
                case "brightness":
                    return brightness;
                case "resolution":
                    return resolution;
                case "fullscreen":
                    return fullscreen;
                case "soundS":
                    return soundS;
                case "controls":
                    return controls;
                case "language":
                    return language;
                case "back":
                    return back;
                case "videoS":
                    return videoS;
                case "effects":
                    return effects;
                case "music":
                    return music;
                case "speech":
                    return speech;
                case "mouseS":
                    return mouseS;
                case "controlsS":
                    return controlsS;
                case "sensitivity":
                    return sensitivity;
                case "invertY":
                    return invertY;
                case "defaultKeys":
                    return defaultKeys;
                case "attackS":
                    return attackS;
                case "equipmentS":
                    return equipmentS;
                case "pressAnyKey":
                    return pressAnyKey;
                case "save":
                    return save;
            }
            return "Undefined";
        }
        set { }
    }
}

[Serializable]
public class Settings
{
    public Setting[] languages;
}