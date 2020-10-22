using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    [System.Serializable]
    public struct PoolStruct
    {
        public Poolable objectToPool;
        public int amountToPool;
        public List<Poolable> objects;
        public List<bool> ready;
    }

    public class Pool : MonoBehaviour
    {
        public PoolStruct pool;

        public void Init()
        {
            if (pool.objects.Count > 0) DestroyPool();
            pool.objects = new List<Poolable>();
            pool.ready = new List<bool>();

            for (int i = 0; i < pool.amountToPool; i++)
            {
                Poolable o = Instantiate(pool.objectToPool, transform);
                o.Initialise();
                o.gameObject.SetActive(false);
                pool.objects.Add(o);
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

        public Poolable GetFirstReady(Vector3? position = null)
        {
            for (int i = 0; i < pool.ready.Count; i++)
            {
                if (pool.ready[i])
                {
                    pool.ready[i] = false;
                    pool.objects[i].gameObject.SetActive(true);
                    if (position != null) pool.objects[i].transform.position = (Vector3)position;
                    pool.objects[i].OnPooled();
                    return pool.objects[i];
                }
            }
            return null;
        }

        public void Restock(Poolable o)
        {
            for (int i = 0; i < pool.objects.Count; i++)
            {
                if (pool.objects[i] == o)
                {
                    pool.ready[i] = true;
                    pool.objects[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
