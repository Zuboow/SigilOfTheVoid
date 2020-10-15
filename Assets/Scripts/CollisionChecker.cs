using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    GameObject newObject;

    void OnEnable()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<OnDestroyDropper>() != null)
        collider.gameObject.GetComponent<OnDestroyDropper>().DropAndDestroy(collider.gameObject);
    }
}