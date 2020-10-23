using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TSVFormatFixer : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        if (importedAssets == null) return;
        if (importedAssets.Length == 0) return;
        for (int i = 0; i < importedAssets.Length; i++)
        {
            if (importedAssets[i].EndsWith(".tsv"))
                TSVFormatFixer.ConvertToCSV(importedAssets[i]);
        }
    }

    static void ConvertToCSV(string path)
    {
        AssetDatabase.RenameAsset(path, path.Replace(".tsv", ".csv"));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        string newPath = path.Substring(0, path.Length - 4);
        newPath += ".csv";
        File.Move(path, newPath);
        File.Move(path + ".meta", newPath + ".meta");

        char separator = ';';
        string content = File.ReadAllText(newPath);
        content = content.Replace('\t', separator);
        File.WriteAllText(newPath, content);
    }
}
