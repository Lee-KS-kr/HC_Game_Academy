using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mizu
{
    public class Enemy : MonoBehaviour, IAttackable
    {
        private int hp;
        public int HP {
            set
            {
                hp = value;

                if (hp < 0)
                {
                    hp = 0;
                    OnDie();
                }
            }
            get => hp;
        }

        public HpDelegate hpDelegate { get; set; }
        public DieDelegate onDieDelegate { get; set; }
        public Action<AudioClip> audioAction;

        [SerializeField] private Animator _animator;
        [SerializeField] private Collider _collider;
        [SerializeField] private Collider[] colliders;
        [SerializeField] private Rigidbody[] rigidbodies;
        [SerializeField] private ScreenUI _screen;
        [SerializeField] private GameObject _blood;
        [SerializeField] private GameObject _explosion;
        [SerializeField] private AudioClip _bloodClip;
        [SerializeField] private AudioClip _explosionClip;
        private Vector3 _target;

        private float _sinkTime = 3f;
        private readonly int hashOnHit = Animator.StringToHash("onHit");

        private void Start()
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].isTrigger = true;
                rigidbodies[i].isKinematic = true;
            }

            _animator.enabled = true;
            HP = 15;
        }

        public void OnHit(int dmg, Vector3 pos)
        {
            HP -= dmg;
            _animator.SetTrigger(hashOnHit);
            _target = pos;
            var dir = _target - transform.position;
            _blood.transform.rotation = Quaternion.LookRotation(dir);
            _explosion.transform.position = transform.position + new Vector3(0, 1.3f, 0);

            audioAction?.Invoke(_bloodClip);
            _blood.SetActive(true);
            audioAction?.Invoke(_explosionClip);
            _explosion.SetActive(true);

            hpDelegate?.Invoke(HP);
        }

        public void OnDie()
        {
            _animator.enabled = false;
            _collider.enabled = false;

            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].isTrigger = false;
                rigidbodies[i].isKinematic = false;
            }

            var dir = transform.position - _target;
            rigidbodies[rigidbodies.Length - 1].AddForce(dir * 10);

            _screen.OnCount();
            onDieDelegate?.Invoke();
            StartCoroutine(SinkDown());
        }

        private IEnumerator SinkDown()
        {
            yield return new WaitForSeconds(_sinkTime);

            foreach (var col in colliders)
                col.isTrigger = true;
        }
    }
}