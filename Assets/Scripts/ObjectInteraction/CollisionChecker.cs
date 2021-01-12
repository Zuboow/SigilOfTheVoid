using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    int minWeaponDamage = 3, maxWeaponDamage = 5;
    GameObject camera;
    void OnEnable()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<EntityHealthManager>() != null)
        {
            collider.gameObject.GetComponent<EntityHealthManager>().ManageDamage(minWeaponDamage, maxWeaponDamage);
            camera.GetComponent<Camera_Movement>().StartShaking();
            camera.GetComponent<AudioPlayer>().PlaySound(8);
        }
        if (collider.gameObject.GetComponent<EntityReactionManager>() != null)
        {
            collider.gameObject.GetComponent<EntityReactionManager>().ManageReaction(collider.gameObject.name, GameObject.FindGameObjectWithTag("Player"));
        }
    }
}