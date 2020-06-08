﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        GetComponent<SpriteRenderer>().sprite = LoadSprite(name);
    }

    // Update is called once per frame
    void Update()
    {
    }

    Sprite LoadSprite(string name)
    {
        if (string.IsNullOrEmpty("Assets/Graphics/Sprites/Objects/" + name + "Icon.png")) return null;
        if (System.IO.File.Exists("Assets/Graphics/Sprites/Objects/" + name + "Icon.png"))
        {
            byte[] bytes = System.IO.File.ReadAllBytes("Assets/Graphics/Sprites/Objects/" + name + "Icon.png");
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            texture.filterMode = FilterMode.Point;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f),256f);
            return sprite;
        }
        return null;
    }
}
