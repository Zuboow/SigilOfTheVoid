using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityHealthManager : MonoBehaviour
{
    public int healthPoints, remainingHealthPoints;
    GameObject healthBar;
    Slider slider;
    DateTime hitTime;

    void OnEnable()
    {
        healthBar = transform.Find("HealthBar").gameObject;
        slider = healthBar.transform.Find("Parent").GetComponent<Slider>();
    }

    void FixedUpdate()
    {
        if (remainingHealthPoints == healthPoints)
        {
            healthBar.SetActive(false);
        }

        if (DateTime.Now.Subtract(hitTime).TotalSeconds > 5)
        {
            healthBar.SetActive(false);
        }
    }

    public void ManageDamage(int minDamage, int maxDamage)
    {
        System.Random randomizer = new System.Random();
        int randomDamage = randomizer.Next(minDamage, maxDamage);
        if (remainingHealthPoints - randomDamage >= 1)
        {
            healthBar.SetActive(true);
            DamageEntity(randomDamage);
        } 
        else
        {
            KillEntity();
        }
    }

    void DamageEntity(int damage)
    {
        remainingHealthPoints -= damage;
        float hp = healthPoints, hpR = remainingHealthPoints, percentage = hpR / hp;
        slider.value = percentage;
        hitTime = DateTime.Now;
    }

    void KillEntity()
    {
        GetComponent<OnDestroyDropper>().DropAndDestroy(gameObject);
    }
}
