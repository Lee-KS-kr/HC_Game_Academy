using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Mizu
{
    public class Director : MonoBehaviour
    {
        private PlayableDirector _director;
        [SerializeField] ScreenUI _screen;

        private void Awake()
        {
            _director = gameObject.GetComponent<PlayableDirector>();
            if(_screen != null)
            {
                _screen.action = null;
                _screen.action += PlayTL;
            }
        }

        public void PlayTL()
        {
            _director.Play();
        }
    }
}