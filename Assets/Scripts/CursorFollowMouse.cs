using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class CursorFollowMouse : MonoBehaviour
    {
        [SerializeField]
        private Transform self;
        [SerializeField]
        private SpriteRenderer sp;
        [SerializeField]
        private Camera myCamera;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            self.position = myCamera.ScreenToWorldPoint(Input.mousePosition);
            self.position = new Vector3(self.position.x, self.position.y, 0);
            self.position -= sp.bounds.size * 0.25f;
        }
    }
}
