using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    public GameObject hero;
    public float followSpeed;
    private Vector3 offset, cameraPosition;
    public float shakeDuration;
    public bool shake = false;

    void OnEnable()
    {
        offset = transform.position - hero.transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, hero.transform.position + offset + new Vector3(0f, 0f, 0f), followSpeed * Time.deltaTime);
        if (shake) Shake();
    }

    public void StartShaking()
    {
        shakeDuration = 0.5f;
        shake = true;
        cameraPosition = transform.localPosition;
    }
    void Shake()
    {
        if (shakeDuration > 0f)
        {
            transform.localPosition = cameraPosition + Random.insideUnitSphere * 0.05f;

            shakeDuration -= Time.deltaTime * 6f;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = cameraPosition;
            shake = false;
        }
    }
}
