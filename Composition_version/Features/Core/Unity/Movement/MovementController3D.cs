using UnityEngine;

namespace Game.Movement
{
    public interface IMove
    {
        public void Move(Vector3 movement);
    }

    // TODO: Extract the logic of moving in the Update of childs
    public abstract class MovementController3D : MonoBehaviour, IMove
    {
        [SerializeField] protected CharacterController _controller;

        [SerializeField] protected float _moveSpeed = 5f;

        protected Vector3 _velocity = new();

        void Awake()
        {
            if (_controller == null)
            {
                Debug.LogError($"{name} requires a CharacterController component.");
                enabled = false;
            }
        }

        public abstract void Move(Vector3 movement);

        protected void DoMove() => _controller.Move(_velocity * Time.deltaTime);
    }

}
