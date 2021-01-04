using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceReference : MonoBehaviour
{
    GameObject camera;
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void PlaySound(int type)
    {
        camera.GetComponent<AudioPlayer>().PlaySound(type);
    }
}
