using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestReader : MonoBehaviour
{
    public TextAsset questFile;
    public Text textUI;
    private List<string>[] fullTable;

    private void Start() {
        string fullFileContents = questFile.text;

        string[] lines = fullFileContents.Split(new string[] { "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        fullTable = new List<string>[lines.Length];

        for (int i = 0; i < lines.Length; i++) {
            fullTable[i] = new List<string>();
            string[] colums = lines[i].Split(';');
            for (int j = 0; j < colums.Length; j++) {
                fullTable[i].Add(colums[j]);
            }
        }
    }

    public Quest GetQuest() {
        Quest q = new Quest(fullTable);
        textUI.text = q.text;
        return q;
    }
}
