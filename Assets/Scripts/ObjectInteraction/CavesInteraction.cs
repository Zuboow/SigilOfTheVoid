using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CavesInteraction : MonoBehaviour
{
    public Vector3 offset;
    public GameObject location, screenBlackener;
    public GameObject player, camera, ambientPlayer;
    bool teleportation = false, teleportated = false, finished = false;
    float timeForBlackening = 1f, timeForWhitening = 1f;
    public AudioClip caveAmbient, seaAmbient;
    public bool caves;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        teleportation = true;        
    }

    void Update()
    {
        if (teleportation)
        {
            if (timeForBlackening > 0f)
            {
                screenBlackener.GetComponent<CanvasGroup>().alpha = timeForBlackening > 0f ? 1 - timeForBlackening : 0;
                timeForBlackening -= Time.deltaTime;
            } else
            {
                if (!teleportated)
                {
                    player.transform.position = new Vector3(location.transform.position.x, location.transform.position.y, player.transform.position.z) + offset;
                    camera.transform.position = new Vector3(location.transform.position.x, location.transform.position.y, camera.transform.position.z) + offset;
                    teleportated = true;
                    if (caves)
                    {
                        ambientPlayer.GetComponent<AudioSource>().clip = caveAmbient;
                        ambientPlayer.GetComponent<AudioSource>().Play();
                        GameSaver.inCaves = true;
                    }
                    else
                    {
                        ambientPlayer.GetComponent<AudioSource>().clip = seaAmbient;
                        ambientPlayer.GetComponent<AudioSource>().Play();
                        GameSaver.inCaves = false;
                    }
                }
                screenBlackener.GetComponent<CanvasGroup>().alpha = timeForWhitening > 0f ? 0 + timeForWhitening : 1;
                timeForWhitening -= Time.deltaTime;
                if (timeForWhitening <= 0)
                {
                    finished = true;
                }
            }

            if (finished)
            {
                teleportation = false;
                teleportated = false;
                finished = false;
                timeForBlackening = 1f;
                timeForWhitening = 1f;
            }

        }
    }
}
