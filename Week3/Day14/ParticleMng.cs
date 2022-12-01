using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public enum Particle
    {
        Bullet = 0,
        Blood,
        Laser,
        SmokeTrail,
    }

    public class ParticleMng : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private GameObject _greenBlood;
        [SerializeField] private GameObject _laser;
        [SerializeField] private GameObject _smokeTrail;
        private GameObject[] pool = new GameObject[4];

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            pool[0] = Instantiate(_bullet, transform);
            pool[1] = Instantiate(_greenBlood, transform);
            pool[2] = Instantiate(_laser, transform);
            pool[3] = Instantiate(_smokeTrail, transform);

            for(int i = 0; i < pool.Length; i++)
                pool[i].SetActive(false);
        }

        public GameObject GetEffect(Particle particle, Transform pos)
        {
            GameObject obj = pool[(int)particle];
            obj.transform.position = pos.position;
            obj.transform.parent = pos;
            obj.SetActive(true);
            return obj;
        }

        public void ReturnEffect(Particle particle)
        {
            GameObject obj = pool[(int)particle];
            obj.transform.position = transform.position;
            obj.transform.parent = transform;
            obj.SetActive(false);
        }
    }
}