using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class BulletBehaviour : Poolable {
        [SerializeField]
        private Transform self;
        [SerializeField]
        private SpriteRenderer sprite;
        [SerializeField]
        private Pool pool;
        [SerializeField]
        private Transform target;
        [SerializeField]
        private BulletDataProfile data;

        private Vector2 direction;

        public override void Initialise() {
            sprite = GetComponent<SpriteRenderer>();
            pool = GetComponentInParent<Pool>();
            direction = Vector2.zero;
            FindObjectOfType<GameManager>().InitBulletTarget(this);
        }

        public override void OnPooled() {
            direction = (target.position - self.position).normalized;
        }

        public void SetTarget(Transform target) {
            this.target = target;
        }

        private void Update() {
            self.Translate(direction * data.speed * Time.deltaTime);
            if (!sprite.isVisible) {
                pool.Restock(this);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if ((collision.gameObject.layer == LayerMask.NameToLayer("Ennemy") && gameObject.name.Contains("NormalBullet")) ||
                (collision.gameObject.layer == LayerMask.NameToLayer("Player") && gameObject.name.Contains("EnnemyBullet"))) {
                pool.Restock(this);
            }
        }
    }
}
