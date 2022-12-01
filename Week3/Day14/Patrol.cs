using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Mizu
{
    public class Patrol : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Enemy enemy;
        [SerializeField] private Transform[] dest;
        [SerializeField] private int destIdx = 0;
        private bool isDead;

        private void Awake()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            enemy = gameObject.GetComponent<Enemy>();
        }

        private void Start()
        {
            if(enemy != null)
            {
                enemy.onDieDelegate -= EnemyDead;
                enemy.onDieDelegate += EnemyDead;
            }

            destIdx = Random.Range(0, dest.Length);
            agent.SetDestination(dest[destIdx].position);
        }

        private void Update()
        {
            if (agent.hasPath || agent.pathPending || isDead)
                return;

            SetNewDest();
        }

        private void SetNewDest()
        {
            int temp = Random.Range(0, dest.Length);
            if(temp == destIdx)
            {
                do
                {
                    temp = Random.Range(0, dest.Length);
                } while (temp == destIdx);
            }

            destIdx = temp;
            agent.SetDestination(dest[destIdx].transform.position);
        }

        private void EnemyDead()
        {
            isDead = true;
            agent.SetDestination(transform.position);
            agent.speed = 0;
        }
    }
}