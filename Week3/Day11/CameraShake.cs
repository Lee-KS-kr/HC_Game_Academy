using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private GameObject _player;
        [SerializeField] private Vector3 _offset;
        private float _shakeTime = 1f;

        private void LateUpdate()
        {
            transform.position = _player.transform.position + _offset;
        }

        public IEnumerator Shake()
        {
            float _time = 0;
            var x = transform.position.x;
            var y = transform.position.y;
            while (_time <= _shakeTime)
            {
                yield return null;

                _time += Time.deltaTime;
                var now = _curve.Evaluate(_time / _shakeTime);
                transform.position =
                    new Vector3(x + now, y + now, transform.position.z);
            }
        }
    }
}