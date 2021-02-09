using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLauncher : MonoBehaviour
{
    public GameObject menuContainer;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (menuContainer.activeInHierarchy)
            {
                case true:
                    menuContainer.SetActive(false);
                    break;
                case false:
                    menuContainer.SetActive(true);
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<InventoryManager>().CloseInventory();
                    break;
            }
        }
    }
}
