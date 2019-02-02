using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Polyneux;

namespace Polyneux
{
    [DI]
    public class Rotator : MonoBehaviour
    {
        private float direction;

        // Start is called before the first frame update
        void Start()
        {
            transform.Rotate(Vector3.up * Random.Range(0f, 360f));
            direction = Random.Range(-200f, 200f);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.up * Time.deltaTime * direction);
        }
    }
}
