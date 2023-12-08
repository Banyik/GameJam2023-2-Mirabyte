using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAudioHandler : MonoBehaviour
    {
        public AudioSource source;
        public AudioClip taser;
        public AudioClip hit;
        public AudioClip miss;

        public void PlayClip(PlayerSounds sound)
        {
            switch (sound)
            {
                case PlayerSounds.Taser:
                    source.clip = taser;
                    break;
                case PlayerSounds.Hit:
                    source.clip = hit;
                    break;
                case PlayerSounds.Miss:
                    source.clip = miss;
                    break;
                default:
                    break;
            }

            source.Play();
        }
    }
}
