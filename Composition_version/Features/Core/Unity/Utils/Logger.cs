using UnityEngine;

namespace Core.Unity.Utils
{
    public class Logger : MonoBehaviour
    {
        public void Log(string text) => Debug.Log(text);
    }

}
