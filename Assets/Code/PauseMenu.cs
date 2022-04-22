using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace Polar
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        GameObject pauseMenu;
        [SerializeField]
        private AudioControl musicControl;

        [SerializeField]
        private AudioControl sfxControl;

        public void Pause()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;

        }
        public void Resume()
        {
            musicControl.Save();
            sfxControl.Save();
            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
            
        }

        public void Home(string sceneName)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadSceneAsync(sceneName);
        }

    }
}
