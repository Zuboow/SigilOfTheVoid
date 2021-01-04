using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityInfo
{
    public string name, reactionType;

    public EntityInfo(string _name, string _reactionType)
    {
        name = _name;
        reactionType = _reactionType;
    }
}

[System.Serializable]
public class EntityInfos
{
    public EntityInfo[] entityInfos;
}
