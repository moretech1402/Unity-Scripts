using UnityEngine;

namespace MC.Core.Unity.Input.Context
{
    public interface IInputContextDefinition
    {
        InputContext Context { get; }
    }

    [CreateAssetMenu(menuName = "Core/Unity/Input/Create new Input Context")]
    public class InputContextSO : ScriptableObject, IInputContextDefinition
    {
        public string ActionMapName;

        InputContext _cached;

        public InputContext Context
        {
            get
            {
                _cached ??= new InputContext(ActionMapName);
                return _cached;
            }
        }
    }

}
