using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Shooter
{
    [CreateAssetMenu(fileName = "New Level Profile", menuName = "Shooter/Level Profile")]
    public class LevelProfile : ScriptableObject
    {
        public static int levelSize = 10;

        [HideInInspector]
        public GameObject[] layer1 = new GameObject[levelSize * levelSize];
        [HideInInspector]
        public GameObject[] layer2 = new GameObject[levelSize * levelSize];

        [HideInInspector]
        public Texture eraser;
        [HideInInspector]
        public GameObject[] objects = new GameObject[0];

    }
}
