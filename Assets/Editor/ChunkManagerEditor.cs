using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Shooter;

[CustomEditor(typeof(ChunkManager))]
public class ChunkManagerEditor : Editor
{
    bool scriptableObjectInitialise = false;
    ChunkManager chunkManager;

    private void OnEnable() {
        chunkManager = target as ChunkManager;
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        if (!scriptableObjectInitialise) PostOnEnable();

        serializedObject.ApplyModifiedProperties();
    }

    private void PostOnEnable() {
        scriptableObjectInitialise = true;
        serializedObject.FindProperty("myCamera").objectReferenceValue = Camera.main;
        Pool[] pools = chunkManager.GetComponents<Pool>();
        serializedObject.FindProperty("pools").ClearArray();
        for (int i = 0; i < pools.Length; i++) {
            serializedObject.FindProperty("pools").InsertArrayElementAtIndex(i);
            serializedObject.FindProperty("pools").GetArrayElementAtIndex(i).objectReferenceValue = pools[i];
        }

    }
}
