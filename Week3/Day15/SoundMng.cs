using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class SoundMng : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private Enemy[] enemy;
        private AudioSource audio;

        private void Start()
        {
            audio = gameObject.GetComponent<AudioSource>();
            if(player != null)
            {
                player.audioAction = null;
                player.audioAction += PlayClip;
            }

            foreach(var e in enemy)
            {
                if(e != null)
                {
                    e.audioAction -= PlayClip;
                    e.audioAction += PlayClip;
                }
            }
        }

        // public static 으로 접근할 수 있게 하는 방법도 있다
        private void PlayClip(AudioClip clip)
        {
            audio.PlayOneShot(clip);
        }
    }
}