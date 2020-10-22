using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChunkLoader))]
public class ChunkLoaderEditor : Editor
{
    public override void OnInspectorGUI() {
        //serializedObject.Update();

        base.OnInspectorGUI();

        ChunkLoader cl = (ChunkLoader)serializedObject.targetObject;

        if (GUILayout.Button("Generate")) cl.Generate();

        serializedObject.ApplyModifiedProperties();
    }
}
