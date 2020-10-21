using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class BackgroundChangeColor : MonoBehaviour
    {

        public Camera myCamera;
        
        float index;
        [Range(0f, 0.1f)]
        public float step;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(ChangeColor());
        }

        IEnumerator ChangeColor()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                index %= 1f;
                myCamera.backgroundColor = Color.HSVToRGB(index, .12f, 1);
                index += step;
            }
        }
    }
}
