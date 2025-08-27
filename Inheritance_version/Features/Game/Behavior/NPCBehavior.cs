using System.Collections.Generic;
using UnityEngine;

namespace Behavior
{
    [RequireComponent(typeof(CharacterMovePathfinding))]
    public class NPCBehavior : MonoBehaviour
    {
        [Header("Config")]
        public float hunger = 0f;
        public float hungerThreshold = 50f;
        public float moveSpeed = 3.5f;

        [Header("Debug")]
        public string currentAction = "Idle";

        private Node behaviorTree;

        CharacterMovePathfinding characterMove;

        public CharacterMovePathfinding CharacterMove => characterMove;

        private void Awake() {
            characterMove = GetComponent<CharacterMovePathfinding>();
        }

        void Start()
        {
            // Construir el árbol de comportamiento
            behaviorTree = new Selector(new List<Node>
        {
            // Priority 1: Hunger
            new Sequence(new List<Node>
            {
                new IsHungryNode(this),
                new FindFoodNode(this),
            }),
            
            // Priority 2: Patrol
            new Sequence(new List<Node>
            {
                new MoveRandomNode(this),
                new WaitNode(this, 0, 3.5f)
            })
        });
        }

        void Update()
        {
            // Execute the tree every frame
            behaviorTree.Evaluate();

            hunger += Time.deltaTime;
        }
    }

}
