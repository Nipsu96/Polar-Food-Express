using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Polar
{
    public class RetryGame : MonoBehaviour
    {
        public void LoadGame(string sceneName)
        {
            SceneManager.LoadScene(sceneName);

            //  Scene scene = SceneManager.GetActiveScene();
            //SceneManager.LoadScene(scene.name);
        }
    }
}
