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
        private float volume = 1f;

        public void PlayClip(PlayerSounds sound, bool overrideSound)
        {
            if(source.isPlaying && !overrideSound)
            {
                return;
            }
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
        private void Update()
        {
            source.volume = volume;
        }
        public void SetVolume(float vol) 
        {
            volume = vol;
        }
    }
}
