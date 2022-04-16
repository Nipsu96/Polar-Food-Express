using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Polar
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadGame(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
