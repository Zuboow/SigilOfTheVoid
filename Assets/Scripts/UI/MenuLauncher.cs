using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLauncher : MonoBehaviour
{
    public GameObject menuContainer, optionsContainer, mainContainer;
    void OnEnable()
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
                    mainContainer.SetActive(false);
                    optionsContainer.SetActive(false);
                    break;
                case false:
                    menuContainer.SetActive(true);
                    mainContainer.SetActive(true);
                    optionsContainer.SetActive(false);
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<InventoryManager>().CloseInventory();
                    break;
            }
        }
    }
}
