using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class BillBoard : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private void Update()
        {
            var dir = transform.position - _camera.transform.position;
            transform.rotation =  Quaternion.LookRotation(dir);
        }
    }
}