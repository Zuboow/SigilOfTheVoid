﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class DamageManager : MonoBehaviour
{
    public Sprite fullHeart;
    public GameObject hero;
    public List<AudioClip> hitEffects = new List<AudioClip>();
    float nextPositionOffset = 0f;
    private List<GameObject> hearts = new List<GameObject>();
    public int healthAmount = 100, maxHealth = 100;

    // Start is called before the first frame update
    void OnEnable()
    {
        RecalculateHearts();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamagePlayer(int damage)
    {
        System.Random rand = new System.Random();

        if (healthAmount + damage > 0)
        {
            healthAmount += damage;
            hero.GetComponent<AudioSource>().clip = hitEffects[rand.Next(0, hitEffects.Count - 1)];
            hero.GetComponent<AudioSource>().Play();
        }
        else
        {
            healthAmount = 0;
            Hero_Movement.alive = false;
            GetComponent<InventoryManager>().CloseInventory();
            hero.GetComponent<AudioSource>().clip = hitEffects[hitEffects.Count - 1];
            hero.GetComponent<AudioSource>().Play();
        }
        StartCoroutine(ShowHealthChange(damage));
        RecalculateHearts();
        
    }

    public void RecalculateHearts()
    {
        nextPositionOffset = 0f;
        foreach (GameObject heart in hearts)
        {
            Destroy(heart);
        }
        hearts.Clear();


        for (int x = 0; x < healthAmount / 20; x++)
        {
            SpawnHeart(fullHeart, 1.3f, false);
        }

        int remainingHeartLife = healthAmount % 20;
        if (remainingHeartLife > 0)
        {
            SpawnHeart(fullHeart, remainingHeartLife * 0.01f * 5f + 0.3f, false);
        }
        for (int y = 0; y < (maxHealth - healthAmount) / 20; y++)
        {
            SpawnHeart(fullHeart, 0.35f, true);
        }
    }

    void SpawnHeart(Sprite sprite, float size, bool isEmpty)
    {
        GameObject newHeart = new GameObject();
        newHeart.AddComponent<SpriteRenderer>();
        newHeart.GetComponent<SpriteRenderer>().sprite = sprite;
        newHeart.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
        newHeart.GetComponent<SpriteRenderer>().color = isEmpty ? new Color(0.79f, 0.17f, 0.21f) : new Color(1f, 1f, 1f);
        newHeart.transform.localScale = new Vector3(size * 0.5f, size * 0.5f, 1);

        GameObject instantiatedHeart = Instantiate(newHeart, new Vector3(transform.localPosition.x - 1.6f + nextPositionOffset, transform.localPosition.y + 0.8f, 10f), Quaternion.identity);
        instantiatedHeart.transform.parent = transform;
        hearts.Add(instantiatedHeart);
        Destroy(newHeart);
        nextPositionOffset += 0.13f;
    }

    IEnumerator ShowHealthChange(int healthChange)
    {
        GameObject healthChangeTextSpawner = new GameObject();
        healthChangeTextSpawner.AddComponent<TextMesh>();
        healthChangeTextSpawner.GetComponent<TextMesh>().text = (healthChange < 0 ? "" : "+") + healthChange;
        healthChangeTextSpawner.GetComponent<TextMesh>().fontSize = 80;
        healthChangeTextSpawner.GetComponent<TextMesh>().characterSize = 0.015f;
        healthChangeTextSpawner.GetComponent<TextMesh>().alignment = TextAlignment.Center;
        healthChangeTextSpawner.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
        GameObject healthChangeText = Instantiate(healthChangeTextSpawner, new Vector3(transform.localPosition.x, transform.localPosition.y, 10f), Quaternion.identity);
        Destroy(healthChangeTextSpawner);
        healthChangeText.GetComponent<TextMesh>().color = healthChange < 0 ? new Color(255, 0, 0, 0) : new Color(0, 255, 0, 0);
        healthChangeText.GetComponent<MeshRenderer>().sortingLayerName = "UI";
        healthChangeText.GetComponent<MeshRenderer>().sortingOrder = 11;
        healthChangeText.transform.parent = transform;

        hero.GetComponent<SpriteRenderer>().color = Color.white;

        for (int x = 0; x < 25; x++)
        {
            yield return new WaitForSeconds(0.01f);
            healthChangeText.transform.position = new Vector3(healthChangeText.transform.position.x, healthChangeText.transform.position.y + 0.01f, healthChangeText.transform.position.z);
            healthChangeText.GetComponent<TextMesh>().color = healthChange < 0 ? new Color(255, 0, 0, x * 0.08f) : new Color(0, 255, 0, x * 0.08f);
            hero.GetComponent<SpriteRenderer>().color = healthChange < 0 ? new Color(1f, 0.04f * x, 0.04f * x) : new Color(0.04f * x, 1f, 0.04f * x, x * 0.08f);
        }

        hero.GetComponent<SpriteRenderer>().color = Color.white;

        Destroy(healthChangeText);
        StopCoroutine(ShowHealthChange(0));
    }

    public void Heal(int healingAmount)
    {
        if (healthAmount > 0)
        {
            StartCoroutine(ShowHealthChange(healingAmount));
            if (healthAmount + healingAmount >= maxHealth)
            {
                healthAmount = maxHealth;
            }
            else
            {
                healthAmount += healingAmount;
            }
            RecalculateHearts();
        }
    }
}
