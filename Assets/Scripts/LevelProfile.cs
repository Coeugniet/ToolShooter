using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    [CreateAssetMenu(fileName = "New Level Profile", menuName = "Shooter/Level Profile")]
    public class LevelProfile : ScriptableObject
    {
        public static int levelSize = 10;

        [HideInInspector]
        public int[] matrix = new int[levelSize * levelSize];

        private void Awake()
        {
            for (int i = 0; i < levelSize; i++)
            {
                for (int j = 0; j < levelSize; j++)
                {
                    matrix[i * levelSize + j] = 0;
                }
            }
        }

    }
}
