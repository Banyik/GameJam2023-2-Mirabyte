using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAudioHandler : MonoBehaviour
    {
        public AudioSource source;
        public AudioSource music;
        public AudioClip taser;
        public AudioClip hit;
        public AudioClip launcher;
        public AudioClip miss;

        public AudioClip mainMusic;
        public AudioClip gameOver;
        public AudioClip shop;
        public AudioClip end;
        private float volume = 1f;

        public void PlayGameOverMusic()
        {
            music.loop = false;
            music.Stop();
            music.clip = gameOver;
            music.Play();
        }

        public void PlayMainMusic()
        {
            music.loop = true;
            music.Stop();
            music.clip = mainMusic;
            music.Play();
        }
        public void PlayShopMusic()
        {
            music.loop = false;
            music.Stop();
            music.clip = shop;
            music.Play();
        }

        public void PlayEndMusic() 
        {
            music.loop = false;
            music.Stop();
            music.clip = end;
            music.Play();
        }

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
                case PlayerSounds.Launcher:
                    source.clip = launcher;
                    break;
                default:
                    break;
            }

            source.Play();
        }
        private void Update()
        {
            source.volume = volume;
            music.volume = volume;
        }
        public void SetVolume(float vol) 
        {
            volume = vol;
        }
    }
}
