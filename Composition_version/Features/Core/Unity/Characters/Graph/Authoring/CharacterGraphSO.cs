using UnityEngine;

namespace MC.Core.Unity.Characters.Graph.Authoring
{
    [CreateAssetMenu(menuName = "Core/Characters/Create new Graph")]
    public class CharacterGraphSO : ScriptableObject
    {
        public NodeSO[] Nodes;
        public ConnectionData[] Connections;
    }
}
