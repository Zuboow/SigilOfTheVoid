using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public List<AudioClip> inventorySoundEffects = new List<AudioClip>();

    public void PlaySound(int type)
    {
        GetComponent<AudioSource>().PlayOneShot(inventorySoundEffects[type - 1]);
    }
}
