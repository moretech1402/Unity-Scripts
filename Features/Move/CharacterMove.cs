using UnityEngine;

namespace Move
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMove : MonoBehaviour
    {
        [SerializeField] float defaultSpeed = 5, defaultRunMult = 2;

        protected bool isRunning;

        new Rigidbody2D rigidbody;

        protected void Move(Vector2 movement) {
            var speed = defaultSpeed * (isRunning ? defaultRunMult : 1);
            rigidbody.velocity = speed * movement;
        }
        
        protected void Run(bool running) => isRunning = running;

        void InitRigidbody(){
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.gravityScale = 0;
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        private void Awake() {
            InitRigidbody();
        }
    }

}
