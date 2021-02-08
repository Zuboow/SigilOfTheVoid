using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string name, questIntroductionLine, questNotFinishedLine, questFinishedLine, questGiver, nextQuest, setAvailability;
    public string[] neededItems, rewards, neededEnemies;
    public int amountOfNeededEnemies;
    public bool noRequirement;
    public Quest(string _name, string _questIntroductionLine, string _questNotFinishedLine, string _questFinishedLine, string _questGiver, string[] _neededItems, 
        string[] _neededEnemies, int _amountOfNeededEnemies, string[] _rewards, string _nextQuest, bool _noRequirement, string _setAvailability)
    {
        name = _name;
        questIntroductionLine = _questIntroductionLine;
        questNotFinishedLine = _questNotFinishedLine;
        questFinishedLine = _questFinishedLine;
        neededItems = _neededItems;
        neededEnemies = _neededEnemies;
        amountOfNeededEnemies = _amountOfNeededEnemies;
        rewards = _rewards;
        nextQuest = _nextQuest;
        questGiver = _questGiver;
        noRequirement = _noRequirement;
        setAvailability = _setAvailability;
    }
}

[System.Serializable]
public class Quests
{
    public Quest[] quests;
}