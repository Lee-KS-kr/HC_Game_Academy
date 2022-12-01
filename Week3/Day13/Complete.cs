using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Mizu
{
    public class Complete : MonoBehaviour
    {
        [SerializeField] Text _complete;
        [SerializeField] Button _button;
        [SerializeField] AnimationCurve _curve;
        private float _winTime = 1f;

        private void OnEnable()
        {
            _button.onClick.AddListener(RetryButton);
            StartCoroutine(CompleteText());
        }

        private IEnumerator CompleteText()
        {
            float elapsed = 0f;
            while (elapsed < _winTime)
            {
                elapsed += Time.deltaTime;
                if(elapsed < _winTime/2)
                {
                    _complete.fontSize = Mathf.Clamp(_complete.fontSize + 10, 150, 300);
                }
                else
                {
                    _complete.fontSize = Mathf.Clamp(_complete.fontSize - 10, 150, 300);
                }

                yield return null;
            }
        }

        private void RetryButton()
        {
            // 페이드 아웃
            // 재시작
            SceneManager.LoadScene(0);
        }
    }
}