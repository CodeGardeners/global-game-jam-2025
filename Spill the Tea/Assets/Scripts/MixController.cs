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
        [SerializeField] private String ambienceVolumeParamName = "AmbienceVolume";
        [SerializeField] private String charactersVolumeParamName = "CharactersVolume";
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
            Camera camera =Camera.main;
            _cameraMovement = camera.GetComponent<CameraMovement>();
            audioMixer.SetFloat(masterVolumeParamName, -80.0f);
            float vol;
            audioMixer.GetFloat(masterVolumeParamName, out vol);
            //Debug.Log(vol);
            
            StartCoroutine(FadeMixerGroup.StartFade(audioMixer, masterVolumeParamName, masterFadeInLengthOnStart, masterVolume));
        }

        private void Update()
        {
            SetAmbienceMix(_cameraMovement.GetCameraHeight());
        }

        private void SetAmbienceMix(float mix)
        {
            float ambOldVol;
            audioMixer.GetFloat(ambienceVolumeParamName, out ambOldVol);
            float ambNewVol = Mathf.Clamp(mix, 0.0001f, 1.0f);

            float charsOldVol;
            audioMixer.GetFloat(charactersVolumeParamName, out charsOldVol);
            float charsNewVol = Mathf.Clamp(mix*-1.0f+1.0f, 0.0001f, 1.0f);

            // FadeMixerGroup.SetGroupVol(
            //     audioMixer,
            //     ambienceVolumeParamName,
            //     ambOldVol,
            //     mix,
            //     mix);
            // FadeMixerGroup.SetGroupVol(
            //     audioMixer,
            //     charactersVolumeParamName,
            //     charsOldVol,
            //     charsNewVol,
            //     mix);            
            FadeMixerGroup.SetGroupVol2(
                audioMixer,
                ambienceVolumeParamName,
                ambNewVol);
            FadeMixerGroup.SetGroupVol2(
                audioMixer,
                charactersVolumeParamName,
                charsNewVol);
            Debug.Log($"charVol: {charsNewVol}");
        }
    }

}
