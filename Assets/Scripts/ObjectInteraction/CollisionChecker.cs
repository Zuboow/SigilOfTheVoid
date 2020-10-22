using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    int weaponDamage = 3;
    void OnEnable()
    {

    }

    private void Update()
    {
        //weaponDamage = 5;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<EntityHealthManager>() != null)
        {
            collider.gameObject.GetComponent<EntityHealthManager>().ManageDamage(weaponDamage);
        }
        if (collider.gameObject.GetComponent<EntityReactionManager>() != null)
        {
            collider.gameObject.GetComponent<EntityReactionManager>().ManageReaction(collider.gameObject.name, GameObject.FindGameObjectWithTag("Player"));
        }
    }
}