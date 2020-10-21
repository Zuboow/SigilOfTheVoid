using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_SpriteManager : MonoBehaviour
{
    public Animator heroAnimator;
    public static bool alive = true;

    void OnEnable()
    {

    }

    void Update()
    {
        heroAnimator.SetBool("alive", alive);

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (alive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                heroAnimator.SetBool("attack", true);
            } 
            else
            {
                heroAnimator.SetBool("attack", false);
            }

            if (x != 0 || y != 0)
            {
                heroAnimator.SetBool("Moving", true);
            }
            else
            {
                heroAnimator.SetBool("Moving", false);
            }

            CheckDirection(x,y);
        }
    }

    void CheckDirection(float x, float y)
    {
        switch (x)
        {
            case var _ when x < 0:
                heroAnimator.SetFloat("Direction", 0.25f);
                break;
            case var _ when x > 0:
                heroAnimator.SetFloat("Direction", 1f);
                break;
            case var _ when x == 0:
                if (y < 0)
                {
                    heroAnimator.SetFloat("Direction", 0f);
                }
                else if (y > 0)
                {
                    heroAnimator.SetFloat("Direction", 0.75f);
                }
                break;
        }
    }
}
