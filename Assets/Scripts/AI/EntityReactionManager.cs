using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityReactionManager : MonoBehaviour
{
    void OnEnable()
    {

    }

    void Update()
    {
        
    }

    public void ManageReaction(string name)
    {
        string reaction = ReadFromFile(name.Split(' ')[0]);
        switch(reaction)
        {
            case "Run":
                Run();
                break;
            case "Attack":
                Attack();
                break;
            default:
                Debug.Log("Undefined entity");
                break;
        }
    }

    void Run()
    {
        Debug.Log("Uciekać");
    }

    void Attack()
    {
        Debug.Log("Zginiesz");
    }

    string ReadFromFile(string entityName)
    {
        TextAsset jsonData = Resources.Load("JSON/entityinfos") as TextAsset;
        EntityInfos entityinfos = JsonUtility.FromJson<EntityInfos>(jsonData.text);
        foreach (EntityInfo i in entityinfos.entityInfos)
        {
            if (name == i.name)
            {
                return i.reactionType;
            }
        }
        return "";
    }
}
