using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    public GameObject hero;
    public float followSpeed;
    private Vector3 offset;

    // Start is called before the first frame update
    void OnEnable()
    {
        offset = transform.position - hero.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, hero.transform.position + offset, followSpeed * Time.deltaTime);
        transform.position = new Vector3(hero.transform.position.x, hero.transform.position.y, -10.0f);
    }
}
