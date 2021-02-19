using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    Vector3 newPosition;
    bool reachedPosition = true, idle = false;
    string reaction;
    Rigidbody2D rigidBody;
    GameObject player, camera;
    Animator entityAnimator;
    float attackTimer = 1f;
    void OnEnable()
    {
        newPosition = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        entityAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        switch (reaction)
        {
            default:
                //BehaveIdle();
                Scan();
                break;
            case "Run":
                Run();
                break;
            case "Attack":
                Attack();
                break;
        }
    }

    public void SetReaction(string newReaction, GameObject playerV)
    {
        reaction = newReaction;
        if (player == null)
            player = playerV;
    }

    IEnumerator Idle(int time)
    {
        idle = true;
        yield return new WaitForSecondsRealtime(time);
        idle = false;
        reachedPosition = true;
        StopAllCoroutines();
    }

    public void Scan()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 1f)
        {
            reaction = "Attack";
        }
    }

    public void Run()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 2f)
        {
            Vector2 newPosition = transform.position - player.transform.position;
            transform.position += new Vector3(newPosition.normalized.x, newPosition.normalized.y, 0f) * 0.5f * Time.deltaTime;
            SetMovement(Vector3.Normalize(new Vector3(transform.position.x, transform.position.y, 0f) - new Vector3(newPosition.normalized.x, newPosition.normalized.y, 0f)));
        }
        else
        {
            reaction = "Idle";
            entityAnimator.SetBool("Moving", false);
            newPosition = transform.position;
            reachedPosition = true;
        }
    }

    public void Attack()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > 1f || DamageManager.healthAmount <= 0)
        {
            reaction = "Idle";
            entityAnimator.SetBool("Moving", false);
        }
        else if (Vector3.Distance(player.transform.position, transform.position) > 0.2f)
        {
            Vector3 reversedVector = (transform.position - player.transform.position) * -1;
            rigidBody.MovePosition(transform.position + 0.5f * reversedVector.normalized * Time.deltaTime);
            SetMovement(Vector3.Normalize(new Vector3(transform.position.x, transform.position.y, 0f) - (player.transform.position + reversedVector)));
        }
        else
        {
            entityAnimator.SetBool("Moving", false);

            if (entityAnimator.GetBool("Attack") == false)
            {
                if (attackTimer <= 0f)
                {
                    entityAnimator.SetBool("Attack", true);
                    camera.GetComponent<DamageManager>().DamagePlayer(Random.Range(-11, -26));
                    camera.GetComponent<Camera_Movement>().StartShaking();
                    attackTimer = 0.5f;
                }
                else
                {
                    attackTimer -= Time.deltaTime;
                    if (attackTimer <= 0f) { attackTimer = 0f; }
                }
            }
        }
    }

    void BehaveIdle()
    {
        if (!idle)
        {
            if (reachedPosition)
            {
                newPosition = newPosition + (Random.insideUnitSphere * 0.5f);
                reachedPosition = false;
            }
            if (transform.position != newPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, newPosition, 0.3f * Time.deltaTime);
            }
            else
            {
                int time = Random.Range(1, 14);
                StartCoroutine(Idle(time));
            }
        }
    }

    void CheckDirection(float x, float y)
    {
        switch (x)
        {
            case var _ when x <= 0.5f && x >= -0.5f:
                if (y < 0)
                {
                    entityAnimator.SetFloat("Direction", 0f);
                }
                else if (y > 0)
                {
                    entityAnimator.SetFloat("Direction", 0.75f);
                }
                break;
            case var _ when x < -0.5f:
                entityAnimator.SetFloat("Direction", 1f);
                break;
            case var _ when x > 0.5f:
                entityAnimator.SetFloat("Direction", 0.25f);
                break;
        }
    }

    void SetAttackAsFalse()
    {
        entityAnimator.SetBool("Attack", false);
    }

    void SetMovement(Vector3 vector)
    {
        if (vector.x != 0 || vector.y != 0)
        {
            entityAnimator.SetBool("Moving", true);
        }
        else
        {
            entityAnimator.SetBool("Moving", false);
        }

        CheckDirection(vector.x, vector.y);
    }
}
