using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Polar
{
    public class SetMusicVolume : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer mixer;

        public void SetLevel(float sliderValue)
        {
            mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);

        }
    }
}
