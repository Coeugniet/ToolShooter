using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class PlayerMovement : MonoBehaviour
    {
        public Transform _self;
        public BoxCollider2D _collider;
        public Rigidbody2D _body;
        public PlayerDataProfile data;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _body.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f) * data.speed * Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ennemy")) {
                GameOver.PauseGame();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Bonus")) {
                Destroy(collision.gameObject);
            }
        }
    }
}
