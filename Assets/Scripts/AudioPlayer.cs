using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public List<AudioClip> inventorySoundEffects = new List<AudioClip>();

    public void PlaySound(int type)
    {
        GetComponent<AudioSource>().clip = inventorySoundEffects[type - 1];
        GetComponent<AudioSource>().Play();
    }
}
