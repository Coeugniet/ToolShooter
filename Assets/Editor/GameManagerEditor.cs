using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Shooter;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor {

    bool scriptableObjectInitialise = false;
    GameManager gameManager;

    private void OnEnable() {
        gameManager = target as GameManager;
    }

    public override void OnInspectorGUI() {
        if (!scriptableObjectInitialise) PostOnEnable();
    }

    private void PostOnEnable() {
        scriptableObjectInitialise = true;
        gameManager.Initialise();
        EditorUtility.SetDirty(gameManager);
    }
}