using System;
using UnityEngine;

namespace Audio
{
    [Serializable]
    public class AudioGuestCharacter : MonoBehaviour
    {

        private bool seated = false;
        
        // while the character sits next to others who are playing the same piece this stays true, else false.
        private bool playsUndistorted = true; 
        
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void ToggleDistortedPlaying()
        {
            playsUndistorted = !playsUndistorted;
            
            return; 
        }
        
        
    }

}