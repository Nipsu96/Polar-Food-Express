using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace Polar
{
    public class PauseMenu : MonoBehaviour
    {
		[SerializeField] private PlayerController playerController;
        [SerializeField]
        GameObject pauseMenu;
        [SerializeField]
        private AudioControl musicControl;

        [SerializeField]
        private AudioControl sfxControl;

		[SerializeField] private GameObject areYouSurePopup;

		public void Pause()
        {
			if(playerController != null)
			{
				playerController.DisableControls();
			}

            Time.timeScale = 0f;
            pauseMenu.SetActive(true);

        }
        public void Resume()
		{
			if (playerController != null)
			{
				playerController.EnableControls();
			}

			pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
            musicControl.Save();
            sfxControl.Save();
            
        }

		public void TryingToQuitMenu()
		{
			areYouSurePopup.SetActive(true);
		}

		public void BackToPauseMenu()
		{
			areYouSurePopup.SetActive(false);
		}

        public void Home(string sceneName)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(sceneName);
        }

		public void OnReloadScene()
		{
			Scene currentScene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(currentScene.name);
		}
    }
}
