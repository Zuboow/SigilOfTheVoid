using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsController : MonoBehaviour
{
    public List<GameObject> optionsObjects;
    Dictionary<string, GameObject> options = new Dictionary<string, GameObject>();
    GameObject activeGameObject = null;
    void Start()
    {
        foreach (GameObject o in optionsObjects)
        {
            o.SetActive(false);
            options.Add(o.name, o);
        }

        activeGameObject = options["Main"];
        activeGameObject.SetActive(true);
    }

    void Update()
    {
        
    }

    public void SetActiveWindow(string name)
    {
        activeGameObject.SetActive(false);
        activeGameObject = options[name];
        activeGameObject.SetActive(true);
    }
}
