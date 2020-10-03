using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Movement : MonoBehaviour
{
    public Animator heroAnimator;
    public float playerSpeed;
    public static bool alive = true;
    new Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void OnEnable()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        heroAnimator.SetBool("alive", alive);

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, y);

        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }


        if (alive)
        {
            rigidbody2D.MovePosition(transform.position + playerSpeed * movement * Time.deltaTime);

            heroAnimator.SetFloat("Horizontal", x);
            heroAnimator.SetFloat("Vertical", y);
        } else
        {
            heroAnimator.SetFloat("Horizontal", 0);
            heroAnimator.SetFloat("Vertical", 0);
        }
    }
}
