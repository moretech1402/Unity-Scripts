using Core.Interfaces;
using Stats;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(StatsHandlerGO))]
    public class CharacterHandler : MonoBehaviour, IConfigurable<CharacterSheet>
    {
        [SerializeField] CharacterSheet template;
        string _name;
        //    AspectHandler _aspectHandler;
        StatsHandlerGO _statsHandler;

        public StatsHandlerGO Stats => _statsHandler;

        public string Name => _name;

        public void Configure(CharacterSheet sheet)
        {
            if (sheet == null) return;
            template = sheet;
            _name = sheet.Name;
            //        _aspectHandler.Configure(sheet.Aspect);
            _statsHandler.Configure(sheet.Stats);
        }

        private void OnEnable()
        {
            //        _aspectHandler = GetComponent<AspectHandler>();
            _statsHandler = GetComponent<StatsHandlerGO>();
            Configure(template);
        }
    }

}
