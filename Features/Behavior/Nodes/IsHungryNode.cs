namespace Behavior
{
    public class IsHungryNode : Node
    {
        private NPCBehavior npc;

        public IsHungryNode(NPCBehavior npc) => this.npc = npc;

        public override NodeStatus Evaluate()
        {
            bool isHungry = npc.hunger >= npc.hungerThreshold;
            return isHungry ? NodeStatus.Success : NodeStatus.Failure;
        }
    }
}