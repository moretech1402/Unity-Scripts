using Core.Events;
using UnityEngine;

namespace Move
{
    public class PlayerMove3D : CharacterMove3D
    {
        [SerializeField] Camera cameraRef;

        private void OnMoveInput(Vector2 movement)
        {
            // Move based on camera direction
            Transform camTransf = cameraRef.transform;
            Vector3 finalMove = camTransf.forward * movement.y + camTransf.right * movement.x;
            finalMove.y = 0;

            Move(finalMove);
        }

        #region Subscribe
        void SubscribeEvents(bool subscribe)
        {
            if (subscribe)
            {
                EventManager.OnInputMove += OnMoveInput;
                EventManager.OnInputJump += Jump;
                EventManager.OnInputRun += Run;
            }
            else
            {
                EventManager.OnInputMove -= OnMoveInput;
                EventManager.OnInputJump -= Jump;
                EventManager.OnInputRun -= Run;
            }
        }
        #endregion

        #region Life Cycle
        private void Start() =>SubscribeEvents(true);

        private void OnDestroy() => SubscribeEvents(false);
        #endregion
    }

}
