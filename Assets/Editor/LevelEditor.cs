using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Shooter
{
    [CustomEditor(typeof(LevelProfile))]
    public class LevelEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Open Level Editor"))
            {
                LevelWindow.Init(target as LevelProfile);
            }
        }
    }
}
