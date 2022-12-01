using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class PlayerIdleState : StateMachineBehaviour
    {
        private readonly int hashRandomIdle = Animator.StringToHash("randomIdle");

        [SerializeField] private float _randomTime = 0;
        [SerializeField] private float _passedTime = 0;
        [SerializeField] private int _randomIdx = 0;

        // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Reset(animator);
            _randomTime = Random.Range(10f, 15f);
        }

        // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _passedTime += Time.deltaTime;
            if (_passedTime >= _randomTime)
            {
                _randomIdx = Random.Range(1, 3);
                animator.SetInteger(hashRandomIdle, _randomIdx);
            }
        }

        // OnStateExit is called before OnStateExit is called on any state inside this state machine
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Reset(animator);
        }

        private void Reset(Animator animator)
        {
            _randomIdx = 0;
            _passedTime = 0;
            animator.SetInteger(hashRandomIdle, _randomIdx);
        }
    }
}