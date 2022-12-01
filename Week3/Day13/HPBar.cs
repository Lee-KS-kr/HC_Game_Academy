using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mizu
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private Image _hpFill;
        [SerializeField] private Image _hpBack;
        [SerializeField] public Enemy _target;
        [SerializeField] private Canvas _canvas;
        private float _fillAmount = 1f;

        // 빌보드 형식으로 구현할 것
        private void Awake()
        {
            _hpBack.fillAmount = _fillAmount;
            _hpFill.fillAmount = _fillAmount;

            // 델리게이트 없애주는거 추가하기..
            if(_target != null)
            {
                _target.hpDelegate -= (HP) => SetAmount(_target.HP);
                _target.onDieDelegate -= SetOff;
                _target.hpDelegate += (HP) => SetAmount(_target.HP);
                _target.onDieDelegate += SetOff;
            }

            _canvas.enabled = false;
        }

        private void SetAmount(int newHp)
        {
            _canvas.enabled = true;

            _fillAmount = (float)newHp / 15;
            StartCoroutine(HPDecrease());
        }

        private IEnumerator HPDecrease()
        {
            _hpFill.fillAmount = _fillAmount;

            while (_hpBack.fillAmount > _fillAmount)
            {
                _hpBack.fillAmount = Mathf.Lerp(_hpBack.fillAmount, _fillAmount, 0.5f * Time.deltaTime);
                yield return null;
            }
        }

        private void SetOff()
        {
            // 예외처리 해주기
            _canvas.enabled = false;
        }
    }
}