using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shooter {
    public class ChunkManager : MonoBehaviour {

        public Pool[] pools;
        public Camera myCamera;

        float chunkSize = 10;
        Queue<ChunkBehaviour> chunks;
        Queue<int> chunksPoolIds;
        Coroutine checkPositionCor;

        private void Start() {
            chunks = new Queue<ChunkBehaviour>();
            chunksPoolIds = new Queue<int>();
            AddNewChunkToTheQueue();
            AddNewChunkToTheQueue();
            AddNewChunkToTheQueue();
            checkPositionCor = StartCoroutine(CheckHeadPosition());
        }

        void AddNewChunkToTheQueue() {
            int chunkPoolId = Random.Range(0, pools.Length);
            ChunkBehaviour chunk = pools[chunkPoolId].GetFirstReady() as ChunkBehaviour;
            if (chunks.Count != 0) {
                chunk.transform.position = chunks.Last().transform.position;
                chunk.transform.Translate(new Vector2(chunkSize, 0));
            } else {
                chunk.transform.Translate(new Vector2(chunkSize, 0));
            }
            chunks.Enqueue(chunk);
            chunksPoolIds.Enqueue(chunkPoolId);
        }

        void RemoveChunkFromQueue() {
            pools[chunksPoolIds.Dequeue()].Restock(chunks.Dequeue());
            AddNewChunkToTheQueue();
        }

        IEnumerator CheckHeadPosition() {
            while (true) {
                yield return new WaitForSeconds(1f);
                float chux = chunks.Peek().transform.position.x;
                float camx = myCamera.transform.position.x;
                if (Mathf.Abs(chux - camx) > (chunkSize + (myCamera.orthographicSize * 2 * (16 / 9)) / 2) + 5) {
                    RemoveChunkFromQueue();
                }
            }
        }

    }
}
