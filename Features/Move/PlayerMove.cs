using Core.Events;
using UnityEngine;

namespace Move
{
    public class PlayerMove : CharacterMove
    {
        private void Start() {
            EventManager.OnInputMove += Move;
        }

        private void OnDestroy() {
            EventManager.OnInputMove -= Move;
        }
    }

}
