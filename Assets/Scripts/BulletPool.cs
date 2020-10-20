using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    [System.Serializable]
    public struct Pool
    {
        public BulletBehaviour objectToPool;
        public int amountToPool;
        public List<BulletBehaviour> objects;
        public List<bool> ready;
    }

    public class BulletPool : MonoBehaviour
    {
        public static BulletPool instance;
        public Pool pool;

        private void Awake()
        {
            instance = this;
        }

        public void Init()
        {
            if (pool.objects.Count > 0) DestroyPool();
            pool.objects = new List<BulletBehaviour>();
            pool.ready = new List<bool>();

            for (int i = 0; i < pool.amountToPool; i++)
            {
                BulletBehaviour b = Instantiate(pool.objectToPool);
                b.Init();
                b.gameObject.SetActive(false);
                pool.objects.Add(b);
                pool.ready.Add(true);
            }
        }

        public void DestroyPool()
        {
            for (int i = 0; i < pool.objects.Count; i++)
            {
                DestroyImmediate(pool.objects[i].gameObject);
            }
            pool.objects.Clear();
            pool.ready.Clear();
        }

        public bool GetFirstReady(Vector3 positon)
        {
            for (int i = 0; i < pool.ready.Count; i++)
            {
                if (pool.ready[i])
                {
                    pool.ready[i] = false;
                    pool.objects[i].gameObject.SetActive(true);
                    pool.objects[i].transform.position = positon;
                    pool.objects[i].UpdateDirection();
                    return true;
                }
            }
            return false;
        }

        public void Restock(BulletBehaviour b)
        {
            for (int i = 0; i < pool.objects.Count; i++)
            {
                if (pool.objects[i] == b)
                {
                    pool.ready[i] = true;
                    pool.objects[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
