using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mizu
{
    public class JoystickController : MonoBehaviour
    {
        [SerializeField] private Image _joyStick;
        [SerializeField] private Image _pad;
        [SerializeField] private GameObject _player;
        private Vector3 _offset;
        private Vector3 _mousePos;
        Vector3 pos = Vector3.zero;
        private float _radius = 70f;
        private bool _isMove = false;

        private void Start()
        {
            _offset = _joyStick.transform.position;
        }

        private void Update()
        {
            OnClickDrag();
            MovePad();
        }

        void OnClickDrag()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isMove = true;
                _mousePos = Input.mousePosition;
                _joyStick.rectTransform.position = _mousePos;
            }
            if (Input.GetMouseButton(0))
            {
                var dir = Input.mousePosition - _mousePos;
                _mousePos = Input.mousePosition;
                var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
                dir.Normalize();
                pos.x = dir.x;
                pos.y = 0;
                pos.z = dir.y;
                _player.transform.rotation = Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z);
                _player.transform.position += pos * Time.deltaTime;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isMove = false;
                _pad.transform.position = _joyStick.rectTransform.position;
                _mousePos = _offset;
                _joyStick.rectTransform.position = _offset;
            }
        }

        void MovePad()
        {
            if (!_isMove) return;

            _pad.transform.position = _mousePos;
            if (Vector3.Distance(_joyStick.transform.position, _pad.transform.position) >= _radius)
            {

            }
        }
    }
}