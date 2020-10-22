using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    [CreateAssetMenu(fileName = "New Bullet Data", menuName = "Shooter/Bullet Data")]
    public class BulletDataProfile : ScriptableObject
    {
        public float speed;
        public float duration;
    }
}
