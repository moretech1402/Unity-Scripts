using UnityEngine;

namespace MC.Core.Unity.UI
{
    public static class UIUtils
    {
        public static void Fresh(Transform parent)
        {
            foreach (Transform child in parent)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}
