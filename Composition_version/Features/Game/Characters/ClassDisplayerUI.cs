using System;
using UnityEngine;
using UnityEngine.Events;

namespace MC.Game.Characters
{
    public class ClassDisplayerUI : MonoBehaviour
    {
        [SerializeField] private UnityEvent<string> _onNameChanged;
        [SerializeField] private UnityEvent<Sprite> _onIconChanged;

        [SerializeField] private Sprite _onNullIcon;

        public ClassSO Class { get; private set; }

        public Action<ClassDisplayerUI> _onClickAction;

        public void SetClass(ClassSO classSO)
        {
            Class = classSO;
            if (classSO == null)
            {
                _onNameChanged?.Invoke(string.Empty);
                _onIconChanged?.Invoke(_onNullIcon);
                return;
            }

            _onNameChanged?.Invoke(classSO.Name);
            _onIconChanged?.Invoke(classSO.Icon);
        }

        public void SetAction(Action<ClassDisplayerUI> action) => _onClickAction = action;

        public void Click() => _onClickAction?.Invoke(this);
    }

}
