using System;
using Core.Events;
using UnityEngine;

namespace Animations
{
    public class GOAnimator : MonoBehaviour
    {

        [Tooltip("Empty if you want animate all parts")]
        [SerializeField] private PartAnimator[] parts;

        private void HandleParts(int id, Action<PartAnimator> partAction)
        {
            if (gameObject.GetInstanceID() != id) return;

            if (parts != null)
                foreach (var part in parts)
                    partAction(part);
        }

        private void OnGOMoved(int id, Vector2 move, float speed) => HandleParts(id, part => part.GOMoved(move, speed));

        private void MoveStopped(int id) => HandleParts(id, part => part.GOStopped());

        private void SubscribeToEvents(bool subscribe = true)
        {
            if (subscribe)
            {
                EventManager.OnGOMoved += OnGOMoved;
                EventManager.OnGOMoveStopped += MoveStopped;
            }
            else
            {
                EventManager.OnGOMoved -= OnGOMoved;
                EventManager.OnGOMoveStopped -= MoveStopped;
            }
        }

        private void OnEnable()
        {
            if (parts == null || parts.Length < 1)
                parts = GetComponentsInChildren<PartAnimator>();

            SubscribeToEvents();
        }

        private void OnDisable() => SubscribeToEvents(false);
    }

}
