using UnityEngine;

namespace MC.Game.Characters
{
    [CreateAssetMenu(fileName = "NewClass", menuName = "Game/Character/Class")]
    public class ClassSO : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
    }
}
