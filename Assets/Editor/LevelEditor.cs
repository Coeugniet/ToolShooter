using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Shooter
{
    [CustomEditor(typeof(LevelProfile))]
    public class LevelEditor : Editor
    {

        bool scriptableObjectInitialise = false;

        private void OnEnable() {
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (!scriptableObjectInitialise) PostOnEnable();

            serializedObject.ApplyModifiedProperties();
            
            base.OnInspectorGUI();

            if (GUILayout.Button("Open Level Editor"))
            {
                LevelWindow.Init(target as LevelProfile);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void PostOnEnable() {
            scriptableObjectInitialise = true;
            serializedObject.FindProperty("eraser").objectReferenceValue = Resources.Load("eraser") as Texture;

            GameObject[] myPrefabs = Resources.LoadAll<GameObject>("Prefabs");
            serializedObject.FindProperty("objects").ClearArray();
            for (int i = 0; i < myPrefabs.Length; i++) {
                serializedObject.FindProperty("objects").InsertArrayElementAtIndex(i);
                serializedObject.FindProperty("objects").GetArrayElementAtIndex(i).objectReferenceValue = myPrefabs[i];
            }
        }
    }
}
