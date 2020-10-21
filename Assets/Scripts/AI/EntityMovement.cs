﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    Vector3 newPosition;
    bool reachedPosition = true, idle = false;
    void OnEnable()
    {
        newPosition = transform.position;
    }

    void Update()
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

    IEnumerator Idle(int time)
    {
        idle = true;
        yield return new WaitForSecondsRealtime(time);
        idle = false;
        reachedPosition = true;
        StopAllCoroutines();
    }
}
