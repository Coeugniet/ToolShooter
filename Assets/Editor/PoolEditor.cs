using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Shooter
{
    [CustomEditor(typeof(Pool))]
    public class PoolEditor : Editor
    {

        SerializedProperty poolProperty;

        public void OnEnable()
        {
            poolProperty = serializedObject.FindProperty("pool");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Pool o = (Pool)serializedObject.targetObject;

            if (GUILayout.Button("Initialise Objects"))
            {
                o.Init();
            }

            if (GUILayout.Button("Destroy Objects"))
            {
                o.DestroyPool();
            }

            EditorGUILayout.PropertyField(poolProperty);
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
