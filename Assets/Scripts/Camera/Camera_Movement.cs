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

    // Start is called before the first frame update
    void OnEnable()
    {
        offset = transform.position - hero.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.Lerp(transform.position, hero.transform.position + offset, followSpeed * Time.deltaTime);
        //transform.position = new Vector3(hero.transform.position.x, hero.transform.position.y, -10.0f);
        if (shake) Shake();
    }

    public void StartShaking()
    {
        shakeDuration = 0.5f;
        shake = true;
        cameraPosition = transform.localPosition;
        Debug.Log("Shake");
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
