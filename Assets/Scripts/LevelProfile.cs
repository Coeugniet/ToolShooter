using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    [CreateAssetMenu(fileName = "New Level Profile", menuName = "Shooter/Level Profile")]
    public class LevelProfile : ScriptableObject
    {
        public int[][] matrix;


    }
}
