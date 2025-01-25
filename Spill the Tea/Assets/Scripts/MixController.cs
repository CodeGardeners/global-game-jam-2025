using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace Audio
{
    public class MixController : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private String masterVolumeParamName = "MasterVolume";
        [SerializeField] public float masterFadeInLengthOnStart = 2.0f;
        [SerializeField] public float masterVolume = 0.0f;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        private void Awake()
        {
            
        }

        void Start()
        {
            audioMixer.SetFloat(masterVolumeParamName, -80.0f);
            float vol;
            audioMixer.GetFloat(masterVolumeParamName, out vol);
            Debug.Log(vol);
            
            StartCoroutine(FadeMixerGroup.StartFade(audioMixer, masterVolumeParamName, masterFadeInLengthOnStart, masterVolume));
            
        }

    }

}
