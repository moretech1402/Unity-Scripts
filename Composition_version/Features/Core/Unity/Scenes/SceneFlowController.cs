using UnityEngine;

namespace Core.Unity.Scenes
{
    public class SceneFlowController : MonoBehaviour
    {
        public static void PushScene(SceneReferenceSO newScene) {
            if (ValidateScene(newScene))
                SceneStacker.Instance.PushScene(newScene.Name);
        }

        public static void ReplaceScene(SceneReferenceSO newScene)
        {
            if (ValidateScene(newScene))
                SceneStacker.Instance.ReplaceScene(newScene.Name);
        }

        private static bool ValidateScene(SceneReferenceSO scene)
        {
            if (scene == null || string.IsNullOrEmpty(scene.Name))
            {
                Debug.LogError("SceneFlowController: Invalid scene reference provided.");
                return false;
            }
            return true;
        }
    }

}
