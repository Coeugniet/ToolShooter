using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter {
    public class EnnemyBehaviour : Poolable {
        [SerializeField]
        private Transform self;
        [SerializeField]
        private Pool pool;
        [SerializeField]
        private float distanceToShoot;
        [SerializeField]
        private float fireRate;

        private Coroutine shootCor;

        public override void Initialise() {
            return;
        }

        public override void OnPooled() {
            return;
        }

        private void Start() {
            shootCor = null;
            pool = GameManager.instance.GetPool("EnnemyBullets");
        }

        private void Update() {
            if (Mathf.Abs(GameManager.instance.GetPlayer().GetPosition() - self.position.x) < distanceToShoot) {
                if (shootCor == null) shootCor = StartCoroutine(Shoot());
            }
        }

        public IEnumerator Shoot() {
            if (pool.GetFirstReady(self.position + new Vector3(-0.25f, 0f)) == null) {
                Debug.LogError("Last ennemy bullet pool return a null bullet, no more ennemy bullet ready in the pool ?");
                Debug.Break();
            }
            yield return new WaitForSeconds(fireRate);
            shootCor = null;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.layer == LayerMask.NameToLayer("Bullet")) {
                this.transform.parent = pool.gameObject.transform;
                if (GameManager.instance.GetCurrentQuestType() == QuestEnum.Kill) GameManager.instance.AddScore(1);
                GameManager.instance.GetPool("Ennemy").Restock(this);
            }
        }

    }
}
