using System;

using UnityEngine;
using UnityEngine.Playables;

namespace Audio
{
    [Serializable]
    public class AudioGuestCharacter : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSourceClean;
        [SerializeField] private AudioSource _audioSourceDistorted;
        private bool seated = false;
        public PlayableDirector playableDirector;

        // while the character sits next to others who are playing the same piece this stays true, else false.
        private bool playsDistorted = true;

        public float crossfadeLength = 1.0f;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetDistortedPlaying(bool distorted)
        {
            playsDistorted = distorted;
            float targetVolClean = (distorted) ? 0.0f : 1.0f;
            float targetVolDistorted = (distorted) ? 1.0f : 0.0f;
            StartCoroutine(AudioLib.FadeAudioSource.StartFade(_audioSourceClean, crossfadeLength, targetVolClean));
            StartCoroutine(AudioLib.FadeAudioSource.StartFade(_audioSourceDistorted, crossfadeLength, targetVolDistorted));
            
        }

        [ContextMenu("DEBUG Crossfade Sources")]
        private void CrossfadeAudioSources()
        {
            float targetVolClean = (_audioSourceClean.volume < 1.0f) ? 1.0f : 0.0f;
            float targetVolDistorted = (_audioSourceDistorted.volume < 1.0f) ? 1.0f : 0.0f;
            StartCoroutine(AudioLib.FadeAudioSource.StartFade(_audioSourceClean, crossfadeLength, targetVolClean));
            StartCoroutine(AudioLib.FadeAudioSource.StartFade(_audioSourceDistorted, crossfadeLength, targetVolDistorted));
        }

    }
}
