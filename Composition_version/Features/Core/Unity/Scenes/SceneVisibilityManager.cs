using UnityEngine;
using UnityEngine.SceneManagement;

namespace MC.Core.Unity.Scenes
{
    public static class SceneVisibilityManager
    {
        public static void SetSceneRootGameObjectsActive(Scene scene, bool active)
        {
            if (!scene.IsValid())
            {
                Debug.LogWarning($"ScenevisityManager: The active state was attempted for an invalid scene.");
                return;
            }

            foreach (GameObject rootObject in scene.GetRootGameObjects())
            {
                rootObject.SetActive(active);
            }
            Debug.Log($"ScenevisityManager: Scene '{scene.name}' Established to active: {active}");
        }
    }
}