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

    public void ManageReaction(string name, GameObject player)
    {
        string reaction = ReadFromFile(name.Split(' ')[0].Trim());
        switch(reaction)
        {
            case "Run":
                GetComponent<EntityMovement>().SetReaction(reaction, player);
                break;
            case "Attack":
                GetComponent<EntityMovement>().SetReaction(reaction, player);
                break;
            default:
                Debug.Log("Undefined entity");
                break;
        }
    }

    string ReadFromFile(string entityName)
    {
        TextAsset jsonData = Resources.Load("JSON/entityinfos") as TextAsset;
        EntityInfos entityinfos = JsonUtility.FromJson<EntityInfos>(jsonData.text);
        foreach (EntityInfo i in entityinfos.entityInfos)
        {
            if (entityName == i.name)
            {
                return i.reactionType;
            }
        }
        return "";
    }
}
