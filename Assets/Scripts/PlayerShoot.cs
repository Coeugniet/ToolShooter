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
            if (!BulletPool.instance.GetFirstReady(self.position + new Vector3(0.25f, 0f))) Debug.Break();
            yield return new WaitForSeconds(1f);
            shootCor = null;
        }
    }
}
