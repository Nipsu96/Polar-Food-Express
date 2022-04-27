using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace Polar
{
    public class OptionsWindow : MonoBehaviour
    {
        [SerializeField]
        private AudioControl musicControl;

        [SerializeField]
        private AudioControl sfxControl;

        [SerializeField]
        private string musicVolumeName;

        [SerializeField]
        private string sfxVolumeName;
        [SerializeField]
        private AudioMixer mixer;

        private void Start()
        {
            musicControl.Setup(mixer, musicVolumeName);
            sfxControl.Setup(mixer, sfxVolumeName);
        }
    }
}
