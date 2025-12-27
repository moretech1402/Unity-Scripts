using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MC.Core.Unity.Scenes
{
    public class SceneStack
    {
        private readonly Stack<Scene> _stack = new();

        public int Count => _stack.Count;

        public void Push(Scene scene)
        {
            if (!_stack.Contains(scene))
            {
                _stack.Push(scene);
                Debug.Log($"SceneStack: Pushed '{scene.name}'. Current count: {_stack.Count}");
            }
            else
            {
                Debug.LogWarning($"SceneStack: Scene '{scene.name}' is already in the stack.");
            }
        }

        public Scene Pop()
        {
            if (_stack.Count == 0)
            {
                Debug.LogWarning("SceneStack: Attempted to pop from an empty stack.");
                return default;
            }
            Scene poppedScene = _stack.Pop();
            Debug.Log($"SceneStack: Popped '{poppedScene.name}'. Current count: {_stack.Count}");
            return poppedScene;
        }

        public Scene Peek()
        {
            if (_stack.Count == 0)
            {
                Debug.LogWarning("SceneStack: Attempted to peek into an empty stack.");
                return default;
            }
            return _stack.Peek();
        }

        public bool Contains(Scene scene)
        {
            return _stack.Contains(scene);
        }
    }
}