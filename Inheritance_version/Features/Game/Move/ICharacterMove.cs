using UnityEngine;

namespace Move
{
    public interface ICharacterMove
    {
        void Run(bool running);
        void Jump();
    }

    public interface ICharacterMove3D : ICharacterMove
    {
        void Move(Vector3 movement);
    }

}
