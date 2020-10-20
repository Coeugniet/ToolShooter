using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class BackgroundChangeColor : MonoBehaviour
    {

        public Camera _camera;
        public float _index;

        float step;

        // Start is called before the first frame update
        void Start()
        {
            step = 100 / 360;
            StartCoroutine(ChangeColor());
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator ChangeColor()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                _camera.backgroundColor = Color.HSVToRGB(_index, .12f, 1);
                _index %= 1;
                _index += step;
            }
        }
    }
}
