using UnityEngine;

namespace Behavior
{
    public class FindFoodNode : Node
    {
        private NPCBehavior npc;

        public FindFoodNode(NPCBehavior npc) => this.npc = npc;

        public override NodeStatus Evaluate()
        {
            // Lógica para encontrar comida (ej: buscar el objeto más cercano con tag "Food")
//            GameObject food = GameObject.FindGameObjectWithTag("Food");
            var bush = GameObject.FindObjectOfType<Bush>();
            if (bush != null)
            {
                npc.currentAction = "Moving to food";
                npc.CharacterMove.MoveTo(bush.gameObject.transform.position);
                npc.hunger = 0;
                return NodeStatus.Success;
            }
            return NodeStatus.Failure;
        }
    }

}
