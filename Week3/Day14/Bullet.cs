using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class Bullet : MonoBehaviour
    {
        // 목표를 알고 있으니까 러프시키기!
        private Rigidbody rigidbody;
        private Vector3 _direction;

        private void Awake()
        {
            rigidbody = gameObject.GetComponent<Rigidbody>();
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            rigidbody.velocity = _direction * 25f;
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    gameObject.SetActive(false);
        //}

        private void OnDisable()
        {
            rigidbody.velocity = Vector3.zero;
        }

        public void SetDirection(Vector3 dir)
        {
            _direction = dir;
        }
    }
}