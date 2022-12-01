using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class MovingObstacle : MonoBehaviour
    {
        [SerializeField] private Transform dest1;
        [SerializeField] private Transform dest2;
        private Vector3 dir;

        private void Update()
        {
            if(Vector3.SqrMagnitude(dest1.position - transform.position) < 0.1f)
            {
                dir = dest2.transform.position - transform.position;
            }
            else if(Vector3.SqrMagnitude(dest2.position - transform.position) < 0.1f)
            {
                dir = dest1.transform.position - transform.position;
            }

            transform.position += dir * Time.deltaTime * 0.5f;
        }
    }
}