using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderController : MonoBehaviour
{
    public GameObject weaponCollider;

    void OnEnable()
    {

    }

    void Update()
    {
        
    }

    void SetWeaponCollider(int type)
    {
        weaponCollider.GetComponent<BoxCollider2D>().enabled = true;
        switch (type)
        {
            case 0:
                weaponCollider.GetComponent<BoxCollider2D>().offset = new Vector2(-0.002830744f, -0.1359826f);
                weaponCollider.GetComponent<BoxCollider2D>().size = new Vector2(0.3007965f, 0.1424822f);
                break;
            case 1:
                weaponCollider.GetComponent<BoxCollider2D>().offset = new Vector2(-0.1276153f, -0.07860667f);
                weaponCollider.GetComponent<BoxCollider2D>().size = new Vector2(0.1483514f, 0.2309831f);
                break;
            case 2:
                weaponCollider.GetComponent<BoxCollider2D>().offset = new Vector2(0.1218059f, -0.06970751f);
                weaponCollider.GetComponent<BoxCollider2D>().size = new Vector2(0.1504412f, 0.2196383f);
                break;
            case 3:
                weaponCollider.GetComponent<BoxCollider2D>().offset = new Vector2(-0.0008523911f, 0.01041597f);
                weaponCollider.GetComponent<BoxCollider2D>().size = new Vector2(0.2889264f, 0.1543525f);
                break;
        }
    }

    void DisableWeaponCollider()
    {
        weaponCollider.GetComponent<BoxCollider2D>().enabled = false;
    }
}
