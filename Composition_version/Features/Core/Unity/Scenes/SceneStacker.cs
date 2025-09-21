using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Core.Unity.Patterns;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace Core.Unity.Scenes
{
    public class SceneStacker : Singleton<SceneStacker>
    {
        [SerializeField] private SceneReferenceSO[] _initialSceneNames;

        // Events
        public event Action<string> OnScenePushed;
        public event Action<string> OnScenePopped;
        public event Action<string> OnSceneReplaced;

        private readonly SceneStack _sceneStack = new();

        [SuppressMessage("SonarAnalyzer.CSharp", "S1144:UnusedPrivateMethod",
        Justification = "Unity Start method called by engine")]
        void Start()
        {
            StartCoroutine(InitializeSceneStackerRoutine());
        }

        /// <summary>
        /// It obtains the name of the scene at the top of the battery (the active/visible scene).
        /// </summary>
        public string GetCurrentSceneName()
        {
            if (_sceneStack.Count > 0)
            {
                return _sceneStack.Peek().name;
            }
            return null;
        }

        /// <summary>
        /// Load a new scene additively and adds it to the top of the battery.
        /// Optionally deactivate the previous scene.
        /// </summary>
        public void PushScene(string sceneName, bool deactivatePrevious = true)
        {
            StartCoroutine(PushSceneRoutine(sceneName, deactivatePrevious));
        }

        /// <summary>
        /// Download the scene at the top of the battery and activate the previous scene.
        /// </summary>
        public void PopScene()
        {
            StartCoroutine(PopSceneRoutine());
        }

        /// <summary>
        /// Replace the current scene at the top of the battery with a new scene.
        /// </summary>
        public void ReplaceScene(string newSceneName)
        {
            StartCoroutine(ReplaceSceneRoutine(newSceneName));
        }

        private static IEnumerator UnloadUnallowedScenes(HashSet<string> allowedScenes)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.IsValid() && scene.isLoaded && !allowedScenes.Contains(scene.name))
                {
                    yield return SceneLoader.UnloadSceneRoutine(scene);
                    Debug.Log($"SceneStacker: Unloaded scene '{scene.name}' because it is not on the initial list.");
                }
            }
        }

        /// <summary>
        /// It establishes the scene given as active in Unity and adds it to the internal battery.
        /// </summary>
        private void ActivateAndPushScene(Scene scene)
        {
            if (!scene.IsValid())
            {
                Debug.LogError($"Scenestacker: Attempt to activate and put an invalid scene.");
                return;
            }

            SceneManager.SetActiveScene(scene);

            _sceneStack.Push(scene);
        }

        private IEnumerator LoadInitialScenesRoutine()
        {
            if (_initialSceneNames != null && _initialSceneNames.Length > 0)
            {
                Debug.Log("SceneStacker: loading specified initial scenes ...");
                foreach (var sceneRef in _initialSceneNames)
                {
                    yield return SceneLoader.LoadSceneAndAddToStackRoutine(
                        sceneRef.Name, LoadSceneMode.Additive, _sceneStack);
                }
            }
        }

        private IEnumerator InitializeSceneStackerRoutine()
        {
            // Current active scene
            Scene activeScene = gameObject.scene;
            _sceneStack.Push(gameObject.scene);

            yield return LoadInitialScenesRoutine();

            // Leave the last stack active
            if (_sceneStack.Count > 0)
            {
                SceneManager.SetActiveScene(_sceneStack.Peek());
            }

            // Disable all scenes that are not the active or the array
            HashSet<string> allowedScenes = new()
            {
                activeScene.name
            };

            if (_initialSceneNames != null)
            {
                foreach (var sceneRef in _initialSceneNames)
                {
                    allowedScenes.Add(sceneRef.Name);
                }
            }

            yield return UnloadUnallowedScenes(allowedScenes);
        }

        private IEnumerator PushSceneRoutine(string sceneName, bool deactivatePrevious)
        {
            if (_sceneStack.Count > 0 && deactivatePrevious)
            {
                SceneVisibilityManager.SetSceneRootGameObjectsActive(_sceneStack.Peek(), true);
            }

            IEnumerator loadOperation = SceneLoader.LoadSceneRoutine(sceneName, LoadSceneMode.Additive);
            yield return loadOperation;

            Scene newScene = SceneManager.GetSceneByName(sceneName);

            if (!newScene.IsValid() || !newScene.isLoaded)
            {
                Debug.LogError($"Scenestacker: The scene '{sceneName}' could not be charged or was not found after loading.");

                if (_sceneStack.Count > 0 && deactivatePrevious)
                {
                    SceneVisibilityManager.SetSceneRootGameObjectsActive(_sceneStack.Peek(), true);
                }
                yield break;
            }

            ActivateAndPushScene(newScene);

            OnScenePushed?.Invoke(sceneName);
        }

        private IEnumerator PopSceneRoutine()
        {
            if (_sceneStack.Count <= 1) // You can't disapprove the last scene (normally the base scene)
            {
                Debug.LogWarning("Cannot pop the last scene in the stack. Ensure you always have a base scene.");
                yield break;
            }

            Scene sceneToUnload = _sceneStack.Pop();

            yield return SceneLoader.UnloadSceneRoutine(sceneToUnload);

            OnScenePopped?.Invoke(sceneToUnload.name);

            // Activate the previous scene if it exists
            if (_sceneStack.Count > 0)
            {
                Scene previousScene = _sceneStack.Peek();
                SceneVisibilityManager.SetSceneRootGameObjectsActive(previousScene, true);
                SceneManager.SetActiveScene(previousScene);
            }
        }

        private IEnumerator ReplaceSceneRoutine(string newSceneName)
        {
            if (_sceneStack.Count == 0)
            {
                Debug.LogWarning("Scenestacker: You can't replace a scene when the battery is empty. Use Pushscene instead.");
                yield break;
            }

            Scene sceneToUnload = _sceneStack.Pop();

            yield return SceneLoader.UnloadSceneRoutine(sceneToUnload);

            Debug.Log($"Scenestacker: Scene '{sceneToUnload.name}' replaced by '{newSceneName}'.");
            OnSceneReplaced?.Invoke(newSceneName);

            yield return SceneLoader.LoadSceneRoutine(newSceneName, LoadSceneMode.Additive);

            Scene newScene = SceneManager.GetSceneByName(newSceneName);
            if (!newScene.IsValid() || !newScene.isLoaded)
            {
                Debug.LogError($"Scenestacker: The scene '{newSceneName}' could not be loaded or was not found after replacement.");
                yield break;
            }

            ActivateAndPushScene(newScene);
        }
    }
}