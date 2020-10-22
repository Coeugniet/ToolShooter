using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Shooter {
    [CustomPropertyDrawer(typeof(PoolStruct))]
    public class PoolDrawer : PropertyDrawer
    {

        SerializedProperty amountToPoolProp;
        int numberOfCol = 10;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            amountToPoolProp = property.FindPropertyRelative("amountToPool");
            float numberOfLines = amountToPoolProp.intValue / numberOfCol;
            return ((numberOfLines + 1) * ((EditorGUIUtility.currentViewWidth - ((numberOfCol - 1) * 3)) / numberOfCol)) + 30;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float x = position.x + 2;
            float y = position.y + 5;
            float w = position.width;
            float h = EditorGUIUtility.singleLineHeight;
            Rect objectToPoolRect = new Rect(x, y, w * 0.48f, h);
            Rect amountToPoolRect = new Rect(x + w * 0.52f, y, w * 0.47f, h);
            Rect listRect = new Rect(x, y + h + 5, w, h * 10);

            SerializedProperty objectToPoolProp = property.FindPropertyRelative("objectToPool");
            SerializedProperty amountToPoolProp = property.FindPropertyRelative("amountToPool");
            if (amountToPoolProp.intValue < 0) amountToPoolProp.intValue = 0;
            SerializedProperty listProp = property.FindPropertyRelative("ready");

            float oldWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth *= 0.4f;
            EditorGUI.PropertyField(objectToPoolRect, objectToPoolProp, new GUIContent("Object"));
            EditorGUI.PropertyField(amountToPoolRect, amountToPoolProp, new GUIContent("Amount"));
            EditorGUIUtility.labelWidth = oldWidth;

            int i = 0;
            int j = 0;
            int k = 0;
            float padding = 3;
            float rw = (EditorGUIUtility.currentViewWidth - 27 - ((numberOfCol - 1) * padding)) / numberOfCol;
            while (k < listProp.arraySize)
            {
                if (j == 10)
                {
                    j = 0;
                    i++;
                }

                if (listProp.GetArrayElementAtIndex(k).boolValue)
                    EditorGUI.DrawRect(new Rect(listRect.x + j * (rw + padding), listRect.y + i * (rw + padding), rw, rw), new Color(0.277f, 0.7f, 0.3888f));
                else EditorGUI.DrawRect(new Rect(listRect.x + j * (rw + padding), listRect.y + i * (rw + padding), rw, rw), new Color(0.7f, 0.277f, 0.277f));

                j++;
                k++;
            }
        }
    }
}