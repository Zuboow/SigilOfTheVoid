using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Movement : MonoBehaviour
{
    public float playerSpeed;
    public static bool alive = true;
    new Rigidbody2D rigidbody2D;

    void OnEnable()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        if (alive)
        {
            rigidbody2D.MovePosition(transform.position + playerSpeed * movement * Time.deltaTime);
        }
    }
}
