using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string name, questIntroductionLine, questNotFinishedLine, questFinishedLine, questGiver;
    public string[] neededItems, rewards;
    public bool completion;
    public Quest(string _name, string _questIntroductionLine, string _questNotFinishedLine, string _questFinishedLine, string _questGiver, string[] _neededItems, string[] _rewards, bool _completion)
    {
        name = _name;
        questIntroductionLine = _questIntroductionLine;
        questNotFinishedLine = _questNotFinishedLine;
        questFinishedLine = _questFinishedLine;
        neededItems = _neededItems;
        rewards = _rewards;
        completion = _completion;
        questGiver = _questGiver;
    }
}

[System.Serializable]
public class Quests
{
    public Quest[] quests;
}