using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.localScale = new Vector3(0.6f, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<SpriteRenderer>().sprite == null)
        {
            GetComponent<SpriteRenderer>().sprite = LoadSprite(name);
        }
    }

    public static Sprite LoadSprite(string name)
    {
        //if (string.IsNullOrEmpty("Assets/Resources/Graphics/Sprites/Objects/" + name + "Icon.png")) return null;
        //if (System.IO.File.Exists("Assets/Resources/Graphics/Sprites/Objects/" + name + "Icon.png"))
        if (Resources.Load<Texture2D>("Graphics/Sprites/Objects/" + name + "Icon") != null)
        {
            //byte[] bytes = System.IO.File.ReadAllBytes("Assets/Resources/Graphics/Sprites/Objects/" + name + "Icon.png");
            Texture2D texture = new Texture2D(1, 1);
            //texture.LoadImage(bytes);
            texture = Resources.Load<Texture2D>("Graphics/Sprites/Objects/" + name + "Icon");
            texture.filterMode = FilterMode.Point;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f),256f);
            return sprite;
        }
        return null;
    }
}
