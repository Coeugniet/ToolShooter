using Shooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter {

    [System.Serializable]
    struct PoolName {
        public string name;
        public Pool pool;
    }

    public class GameManager : MonoBehaviour {

        public static GameManager instance;

        [SerializeField]
        private PlayerMovement player;
        [SerializeField]
        private CursorFollowMouse cursor;
        [SerializeField]
        private List<PoolName> pools;
        [SerializeField]
        private Canvas UI;
        [SerializeField]
        private ChunkManager chunkManager;
        [SerializeField]
        private QuestReader questReader;

        public int score;

        Quest currentQuest;

        public void Awake() {
            instance = this;
        }

#if UNITY_EDITOR
        public void Initialise() {
            Debug.Log("GameManager Initialisation ...");
            this.player = FindObjectOfType<PlayerMovement>();

            this.cursor = FindObjectOfType<CursorFollowMouse>();

            this.pools = new List<PoolName>();
            GameObject poolsObject = GameObject.Find("Pools");
            for (int i = 0; i < poolsObject.transform.childCount; i++) {
                PoolName p;
                p.name = poolsObject.transform.GetChild(i).name;
                p.pool = poolsObject.transform.GetChild(i).GetComponent<Pool>();
                this.pools.Add(p);
            }

            this.UI = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();

            this.chunkManager = FindObjectOfType<ChunkManager>();

            this.questReader = FindObjectOfType<QuestReader>();

            Debug.Log("GameManager Fully Initialise");
        }
#endif

        private void Start() {
            instance.UI.gameObject.SetActive(false);
            instance.NewQuest();
        }


        public void InitBulletTarget(BulletBehaviour bullet) {
            if (bullet.gameObject.name.Contains("Ennemy")) bullet.SetTarget(this.player.transform);
            else bullet.SetTarget(this.cursor.transform);
        }

        public PlayerMovement GetPlayer() {
            return instance.player;
        }

        public CursorFollowMouse GetCursor() {
            return instance.cursor;
        }

        public Pool GetPool(string poolName) {
            for (int i = 0; i < this.pools.Count; i++) {
                PoolName p = this.pools[i];
                if (p.name == poolName) return p.pool;
            }
            return null;
        }

        public void PauseGame() {
            Time.timeScale = 0;
            this.UI.gameObject.SetActive(true);
        }

        public void UnpauseGame() {
            Time.timeScale = 1;
            this.UI.gameObject.SetActive(false);
        }

        public void QuitGame() {
            Application.Quit();
        }

        public void AddScore(int score) {
            this.score += score;
            if (this.score >= currentQuest.goal) PauseGame();
        }

        public QuestEnum GetCurrentQuestType() {
            return currentQuest.type;
        }

        public void NewQuest() {
            currentQuest = questReader.GetQuest();
        }

        public void Restart() {
            chunkManager.OnRestart();
            score = 0;
            UnpauseGame();
        }
    }
}
