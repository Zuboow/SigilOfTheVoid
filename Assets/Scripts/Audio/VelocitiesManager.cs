using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VelocitiesManager : MonoBehaviour
{
    public GameObject[] effectSources, musicSources, speechSources;
    public GameObject effectSlider, musicSlider, speechSlider, effectsTester, speechTester;
    public float effectVelocity = 100, musicVelocity = 100, speechVelocity = 100;

    void OnEnable()
    {
        File.ReadAllText(Application.dataPath + "/Resources/velocityConfig.txt");
        StreamReader tr = new StreamReader(Application.dataPath + "/Resources/velocityConfig.txt", true);
        String[] velocities =  tr.ReadLine().Trim().Split('-');
        tr.Close();
        effectVelocity = Int32.Parse(velocities[0]);
        musicVelocity = Int32.Parse(velocities[1]);
        speechVelocity = Int32.Parse(velocities[2]);
        if (effectSlider != null) effectSlider.GetComponent<UnityEngine.UI.Slider>().value = effectVelocity;
        if (musicSlider != null) musicSlider.GetComponent<UnityEngine.UI.Slider>().value = musicVelocity;
        if (speechSlider != null) speechSlider.GetComponent<UnityEngine.UI.Slider>().value = speechVelocity;
        UpdateVelocites(true, true, true);
        effectsTester.GetComponent<AudioSource>().mute = true;
        speechTester.GetComponent<AudioSource>().mute = true;
    }

    void Update()
    {
    }

    public void UpdateVelocites(bool effects, bool music, bool speech)
    {
        File.WriteAllText(Application.dataPath + "/Resources/velocityConfig.txt", String.Empty);
        TextWriter tw = new StreamWriter(Application.dataPath + "/Resources/velocityConfig.txt", true);
        tw.WriteLine(effectVelocity + "-" + musicVelocity + "-" + speechVelocity);
        tw.Close();

        if (effects)
        {
            foreach (GameObject effectObject in effectSources)
            {
                effectObject.GetComponent<AudioSource>().volume = effectVelocity * 0.01f;
            }
        }
        if (music)
        {
            foreach (GameObject musicObject in musicSources)
            {
                musicObject.GetComponent<AudioSource>().volume = musicVelocity * 0.01f;
            }
        }
        if (speech)
        {
            foreach (GameObject speechObject in speechSources)
            {
                speechObject.GetComponent<AudioSource>().volume = speechVelocity * 0.01f;
            }
        }
    }

    public void PlayTestedEffect()
    {
        effectsTester.GetComponent<AudioSource>().mute = false;
        effectsTester.GetComponent<AudioSource>().Play();
    }

    public void PlayTestedSpeech()
    {
        speechTester.GetComponent<AudioSource>().mute = false;
        speechTester.GetComponent<AudioSource>().Play();
    }

    public void UpdateEffects(float value)
    {
        effectVelocity = value;
        UpdateVelocites(true, false, false);
    }
    public void UpdateMusic(float value)
    {
        musicVelocity = value;
        UpdateVelocites(false, true, false);
    }
    public void UpdateSpeech(float value)
    {
        speechVelocity = value;
        UpdateVelocites(false, false, true);
    }
}
