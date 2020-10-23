using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    Vector3 newPosition;
    bool reachedPosition = true, idle = false;
    string reaction;
    Rigidbody2D rigidBody;
    GameObject player;
    void OnEnable()
    {
        newPosition = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        switch (reaction)
        {
            default:
                BehaveIdle();
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

    public void Run()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 2f)
        {
            Vector2 newPosition = transform.position - player.transform.position;
            transform.position += new Vector3(newPosition.normalized.x, newPosition.normalized.y, 0f) * 0.5f * Time.deltaTime;
        }
        else
        {
            reaction = "Idle";
            newPosition = transform.position;
            reachedPosition = true;
        }
    }

    public void Attack()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > 0.2f)
        {
            Vector3 reversedVector = (transform.position - player.transform.position) * -1;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + reversedVector, 0.6f * Time.deltaTime);
            Debug.Log("Gonię gracza");
        }
        else
        {
            Debug.Log("Dziobię");
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
}
