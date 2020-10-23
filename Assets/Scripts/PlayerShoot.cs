using Shooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField]
        private Transform self;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private Pool pool;
        [SerializeField]
        private float fireRate;

        private Coroutine shootCor;

        private void Start()
        {
            shootCor = null;
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                _animator.Play("Shoot");
                if (shootCor == null) shootCor = StartCoroutine(Shoot());
                
            }
            else
            {
                _animator.Play("Idle");
            }
        }

        public IEnumerator Shoot()
        {
            if (pool.GetFirstReady(self.position + new Vector3(0.25f, 0f)) == null) {
                Debug.LogError("Last bullet pool return a null bullet, no more bullet ready in the pool ?");
                Debug.Break();
            }
            yield return new WaitForSeconds(fireRate);
            shootCor = null;
        }
    }
}
