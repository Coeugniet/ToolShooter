using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Quest
{

    public QuestEnum type;
    public string text;
    public int goal;

    public Quest(List<string>[] table) {
        int i = UnityEngine.Random.Range(1, table.Length);

        int x = UnityEngine.Random.Range(int.Parse(table[i][4]), int.Parse(table[i][5]));
        goal = x;
        if (i == 1) type = QuestEnum.Kill;
        if (i == 2) type = QuestEnum.Score;
        text = table[i][3].Replace("$number", x.ToString());
    }
}

public enum QuestEnum { Score, Kill};
