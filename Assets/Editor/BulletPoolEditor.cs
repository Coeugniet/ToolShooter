using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Shooter
{
    [CustomEditor(typeof(BulletPool))]
    public class BulletPoolEditor : Editor
    {

        SerializedProperty poolProperty;

        public void OnEnable()
        {
            poolProperty = serializedObject.FindProperty("pool");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            BulletPool b = (BulletPool)serializedObject.targetObject;

            if (GUILayout.Button("Initialise Bullets"))
            {
                b.Init();
            }

            if (GUILayout.Button("Destroy Bullets"))
            {
                b.DestroyPool();
            }

            EditorGUILayout.PropertyField(poolProperty);
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
