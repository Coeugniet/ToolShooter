using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Shooter/Player Data")]
    public class PlayerDataProfile : ScriptableObject
    {
        public float speed;
    }
}
