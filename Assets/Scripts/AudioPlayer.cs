using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public List<AudioClip> inventorySoundEffects = new List<AudioClip>();

    public void PlaySound(int type)
    {
        switch (type)
        {
            case 1:
                GetComponent<AudioSource>().clip = inventorySoundEffects[0];
                GetComponent<AudioSource>().Play();
                break;
            case 2:
                GetComponent<AudioSource>().clip = inventorySoundEffects[1];
                GetComponent<AudioSource>().Play();
                break;
            case 3:
                GetComponent<AudioSource>().clip = inventorySoundEffects[2];
                GetComponent<AudioSource>().Play();
                break;
            case 4:
                GetComponent<AudioSource>().clip = inventorySoundEffects[3];
                GetComponent<AudioSource>().Play();
                break;
        }
    }
}
