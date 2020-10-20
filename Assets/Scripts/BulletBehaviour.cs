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

        private void Start()
        {
            cursor = FindObjectOfType<CursorFollowMouse>().gameObject.transform;
            sprite = GetComponent<SpriteRenderer>();
            UpdateDirection();
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
