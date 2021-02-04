using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLoader : MonoBehaviour
{
    void OnEnable()
    {
        transform.localScale = new Vector3(0.6f, 0.6f);
    }

    void Update()
    {
        if (GetComponent<SpriteRenderer>().sprite == null)
        {
            GetComponent<SpriteRenderer>().sprite = LoadSprite(name);
        }
    }

    public static Sprite LoadSprite(string name)
    {
        if (Resources.Load<Texture2D>("Graphics/Sprites/Objects/" + name.Split('(')[0].Trim() + "Icon") != null)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture = Resources.Load<Texture2D>("Graphics/Sprites/Objects/" + name.Split('(')[0].Trim() + "Icon");
            texture.filterMode = FilterMode.Point;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f),256f);
            return sprite;
        }
        return null;
    }
}
