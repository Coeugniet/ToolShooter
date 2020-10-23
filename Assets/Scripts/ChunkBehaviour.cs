using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter {
    public class ChunkBehaviour : Poolable {

        private Camera myCamera;
        private Transform self;

        [SerializeField]
        private Pool ennemyPool;
        [SerializeField]
        private Pool coinPool;
        [SerializeField]
        private Pool diamondPool;

        public ChunkDataProfile data;
        public List<Transform> eSpawnList;
        public List<Transform> cSpawnList;
        public List<Transform> dSpawnList;


        public override void Initialise() {
            myCamera = Camera.main;
            self = transform;
            self.position = myCamera.transform.position + new Vector3(0, -4.5f, 0);
            self.position = new Vector3(self.position.x, self.position.y, 0);

            ennemyPool = FindObjectOfType<GameManager>().GetPool("Ennemy");
            coinPool = FindObjectOfType<GameManager>().GetPool("Coin");
            diamondPool = FindObjectOfType<GameManager>().GetPool("Diamond");
        }

        public override void OnPooled() {
            PoolObjects();
        }

        private void Update() {
            transform.Translate(new Vector3(-1, 0, 0) * data.speed * Time.deltaTime);
        }

        public void PoolObjects() {
            for(int i = 0; i < eSpawnList.Count; i++) {
                Poolable ennemy = ennemyPool.GetFirstReady(eSpawnList[i].position);
                ennemy.transform.SetParent(eSpawnList[i]);
            }
            for (int i = 0; i < cSpawnList.Count; i++) {
                Poolable coin = coinPool.GetFirstReady(cSpawnList[i].position);
                coin.transform.SetParent(cSpawnList[i]);
            }
            for (int i = 0; i < dSpawnList.Count; i++) {
                Poolable diamond = diamondPool.GetFirstReady(dSpawnList[i].position);
                diamond.transform.SetParent(dSpawnList[i]);
            }
        }

        public void RestockObjects() {
            for (int i = 0; i < eSpawnList.Count; i++) {
                if (eSpawnList[i].childCount > 0) {
                    EnnemyBehaviour ennemy = eSpawnList[i].GetChild(0).GetComponent<EnnemyBehaviour>();
                    ennemy.transform.SetParent(ennemyPool.gameObject.transform);
                    ennemyPool.Restock(ennemy);
                }
            }
            for (int i = 0; i < cSpawnList.Count; i++) {
                if (cSpawnList[i].childCount > 0) {
                    CoinBehaviour coin = cSpawnList[i].GetChild(0).GetComponent<CoinBehaviour>();
                    coin.transform.SetParent(coinPool.gameObject.transform);
                    coinPool.Restock(coin);
                }
            }
            for (int i = 0; i < dSpawnList.Count; i++) {
                if (dSpawnList[i].childCount > 0) {
                    DiamondBehaviour diamond = dSpawnList[i].GetChild(0).GetComponent<DiamondBehaviour>();
                    diamond.transform.SetParent(diamondPool.gameObject.transform);
                    diamondPool.Restock(diamond);
                }
            }
        }
    }
}
