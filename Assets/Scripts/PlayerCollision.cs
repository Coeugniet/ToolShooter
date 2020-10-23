using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Shooter {
    public class PlayerCollision : MonoBehaviour {

        public Pool coinPool;
        public Pool diamondPool;

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ennemy")) {
                GameManager.instance.PauseGame();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Bonus")) {
                if (collision.gameObject.name.Contains("Coin")) {
                    if (GameManager.instance.GetCurrentQuestType() == QuestEnum.Score) GameManager.instance.AddScore(1);
                    coinPool.Restock(collision.GetComponent<CoinBehaviour>());
                } else if (collision.gameObject.name.Contains("Diamond")) {
                    if (GameManager.instance.GetCurrentQuestType() == QuestEnum.Score) GameManager.instance.AddScore(5);
                    diamondPool.Restock(collision.GetComponent<DiamondBehaviour>());
                }
            }

            if (collision.gameObject.layer == LayerMask.NameToLayer("EnnemyBullet")) {
                GameManager.instance.PauseGame();
            }
        }
    }
}
