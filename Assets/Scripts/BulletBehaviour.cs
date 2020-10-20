using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Transform self;
        [SerializeField]
        private Transform cursor;
        [SerializeField]
        private SpriteRenderer sprite;

        public BulletDataProfile data;

        private Vector2 direction;

        public void Init()
        {
            cursor = FindObjectOfType<CursorFollowMouse>().gameObject.transform;
            sprite = GetComponent<SpriteRenderer>();
            direction = Vector2.zero;
        }

        public void UpdateDirection()
        {
            direction = (cursor.position - self.position).normalized;
        }

        private void Update()
        {
            self.Translate(direction * data.speed * Time.deltaTime);
            if (!sprite.isVisible)
            {
                BulletPool.instance.Restock(this);
            }
        }
    }
}
