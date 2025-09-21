using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Unity.Scenes
{
    public static class SceneLoader
    {
        public static IEnumerator LoadSceneRoutine(string sceneName, LoadSceneMode mode)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogWarning("SceneLoader: The name of the scene is null or empty.");
                yield break;
            }

            Scene loadedScene = SceneManager.GetSceneByName(sceneName);
            if (loadedScene.isLoaded && loadedScene.IsValid())
            {
                Debug.Log($"SceneLoader: The scene '{sceneName}' is already loaded.");
                yield break;
            }

            var asyncLoad = SceneManager.LoadSceneAsync(sceneName, mode);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            // It is necessary to re-alage the method to ensure that the scene has been loaded correctly.
            if (!SceneManager.GetSceneByName(sceneName).IsValid())
            {
                Debug.LogError($"SceneLoader: The scene '{sceneName}' could not be charged or was not found after loading.");
            }
            else
            {
                Debug.Log($"SceneLoader: Scene '{sceneName}' Loaded success.");
            }
        }

        /// <summary> Download a scene asynchronously. </summary>
        public static IEnumerator UnloadSceneRoutine(Scene scene)
        {
            if (!scene.IsValid() || !scene.isLoaded)
            {
                Debug.LogWarning($"SceneLoader: It was tried to download an invalid or not loaded scene: '{scene.name}'.");
                yield break;
            }

            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(scene);
            while (!asyncUnload.isDone)
            {
                yield return null;
            }

            Debug.Log($"SceneLoader: Scene '{scene.name}' Successfully downloaded.");
        }

        public static IEnumerator LoadSceneAndAddToStackRoutine(string sceneName, LoadSceneMode mode,
        SceneStack sceneStack)
        {
            yield return LoadSceneRoutine(sceneName, mode);

            Scene loadedScene = SceneManager.GetSceneByName(sceneName);
            if (loadedScene.IsValid() && loadedScene.isLoaded)
            {
                sceneStack.Push(loadedScene);
            }
        }
    }
}