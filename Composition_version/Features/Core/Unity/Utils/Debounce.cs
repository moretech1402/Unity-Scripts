using System;
using System.Collections;
using UnityEngine;

namespace Core.Unity.Utils
{
    public class Debounce
    {
        MonoBehaviour _owner;
        float _delayTime;
        bool _recentChanged = false;

        public Debounce(MonoBehaviour owner, float delayTime)
        {
            _owner = owner;
            _delayTime = delayTime;
        }

        public void Execute(Action action)
        {
            if (_recentChanged)
            {
                Debug.LogWarning($"Action {action.Method.Name} ignored due to recent change.");
                return;
            }

            _recentChanged = true;
            Debug.Log($"Action requested: {action.Method.Name} at {DateTime.Now}");
            _owner.StartCoroutine(DebounceCoroutine(action));
        }

        IEnumerator DebounceCoroutine(Action action)
        {
            yield return new WaitForSeconds(_delayTime);
            _recentChanged = false;
            Debug.Log($"Action executed: {action.Method.Name} at {DateTime.Now}");
            action.Invoke();
        }
    }

}