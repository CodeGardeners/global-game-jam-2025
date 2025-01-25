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
        [SerializeField] private CameraMovement _cameraMovement;
        private float _cameraHeight;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        private void Awake()
        {
            
        }

        void Start()
        {
            _cameraMovement = Camera.main.GetComponent<CameraMovement>();
            audioMixer.SetFloat(masterVolumeParamName, -80.0f);
            float vol;
            audioMixer.GetFloat(masterVolumeParamName, out vol);
            Debug.Log(vol);
            
            StartCoroutine(FadeMixerGroup.StartFade(audioMixer, masterVolumeParamName, masterFadeInLengthOnStart, masterVolume));
        }

        private void Update()
        {
            SetAmbienceMix(_cameraMovement.GetHeight());
        }

        private void SetAmbienceMix(float mix)
        {
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            
        }
    }

}
