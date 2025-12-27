using UnityEngine;

namespace MC.Core.Unity.Scenes
{
    // It can be StringReferenceSO to make it more generic but I have preferred clarity
    [CreateAssetMenu(menuName = "Core/Scene Reference")]
    public class SceneReferenceSO : ScriptableObject
    {
        public string Name;
    }

}
