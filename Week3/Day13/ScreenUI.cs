using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mizu
{
    public class ScreenUI : MonoBehaviour
    {
        [SerializeField] private GameObject _finish;
        [SerializeField] private Text _count;
        [SerializeField] private int _enemyCount;
        private int count;
        public int Count
        {
            get => count;
            set
            {
                count = value;
                if (count == _enemyCount)
                    SetComplete();
            }
        }
        public Action action;

        private void Awake()
        {
            _count.text = $"0 / {_enemyCount}";
            _finish.SetActive(false);
        }

        public void OnCount()
        {
            Count++;
            _count.text = $"{Count} / {_enemyCount}";
        }

        private void SetComplete()
        {
            //_finish.SetActive(true);
            action?.Invoke(); // 이름 변경
        }
    }
}