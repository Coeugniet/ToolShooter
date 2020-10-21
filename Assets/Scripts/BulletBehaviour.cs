using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class BulletBehaviour : Poolable
    {
        [SerializeField]
        private Transform self;
        [SerializeField]
        private Transform cursor;
        [SerializeField]
        private SpriteRenderer sprite;
        [SerializeField]
        private Pool pool;

        public BulletDataProfile data;
        private Vector2 direction;

        public override void Initialise()
        {
            cursor = FindObjectOfType<CursorFollowMouse>().gameObject.transform;
            sprite = GetComponent<SpriteRenderer>();
            pool = GetComponentInParent<Pool>();
            direction = Vector2.zero;
        }

        public override void OnPooled()
        {
            direction = (cursor.position - self.position).normalized;
        }

        private void Update()
        {
            self.Translate(direction * data.speed * Time.deltaTime);
            if (!sprite.isVisible)
            {
                pool.Restock(this);
            }
        }
    }
}
