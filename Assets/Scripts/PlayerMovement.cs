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

        // Update is called once per frame
        void Update()
        {
            _body.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f) * data.speed * Time.deltaTime;
        }

        public float GetPosition() {
            return _self.position.x;
        }
    }
}
