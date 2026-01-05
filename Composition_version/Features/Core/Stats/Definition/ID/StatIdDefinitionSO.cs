using UnityEditor;
using UnityEngine;

namespace MC.Core.Stats.Definition
{
    public interface IStatIdDefinition
    {
        StatId StatId { get; }
    }

    [CreateAssetMenu(menuName = "Core/Stats/Create new Stat")]
    public class StatIdDefinitionSO : ScriptableObject, IStatIdDefinition
    {
        [SerializeField, HideInInspector]
        private string _uid;

        public string Name;
        public string Key;

        private StatId _cached;

        public StatId StatId
        {
            get
            {
                _cached ??= new(_uid);
                return _cached;
            }
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(_uid))
            {
                string path = AssetDatabase.GetAssetPath(this);
                _uid = AssetDatabase.AssetPathToGUID(path);
                EditorUtility.SetDirty(this);
            }
        }
#endif

    }
}
