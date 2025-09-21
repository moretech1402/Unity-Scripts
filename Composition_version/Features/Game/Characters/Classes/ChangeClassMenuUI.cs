using System.Collections.Generic;
using MC.Core;
using MC.Core.Unity.Patterns;
using MC.Core.Unity.UI;
using UnityEngine;
using UnityEngine.Events;

namespace MC.Game.Characters.Classes
{
    /// <summary> UI for changing character classes. </summary>
    public class ChangeClassMenuUI : MonoBehaviour
    {
        [SerializeField] private ClassDisplayerUI _equippedClassPrefab;
        [SerializeField] private Transform _equippedClassParent;
        [SerializeField] private ClassDisplayerUI _availableClassPrefab;
        [SerializeField] private Transform _availableClassParent;

        [SerializeField] private UnityEvent<bool> _onValidate;

        private ICharacterService _characterService;

        private PooledDisplayerGroup<ClassDisplayerUI> _equippedDisplayers;
        private PooledDisplayerGroup<ClassDisplayerUI> _availableDisplayers;

        void Awake()
        {
            UIUtils.Fresh(_equippedClassParent);
            UIUtils.Fresh(_availableClassParent);

            _characterService = ServiceLocator.Get<ICharacterService>();

            _equippedDisplayers = new PooledDisplayerGroup<ClassDisplayerUI>(
                _equippedClassPrefab, _equippedClassParent, Unequip);
            _availableDisplayers = new PooledDisplayerGroup<ClassDisplayerUI>(
                _availableClassPrefab, _availableClassParent, Equip);
        }

        void Start()
        {
            _equippedDisplayers.Initialize(_characterService.ChangeableClasses);
            _availableDisplayers.Initialize(_characterService.InStorageClasses);
        }

        public void Equip(ClassDisplayerUI classDisplayer)
        {
            if (classDisplayer.Class == null) return;

            if (_characterService.TryEquip(classDisplayer.Class))
                RefreshUI();

            ValidateAndEmit();
        }

        public void Unequip(ClassDisplayerUI classDisplayer)
        {
            if (classDisplayer.Class == null) return;

            _characterService.SaveInStorage(classDisplayer.Class);
            RefreshUI();

            ValidateAndEmit();
        }

        private void RefreshUI()
        {
            _equippedDisplayers.Refresh(_characterService.ChangeableClasses);
            _availableDisplayers.Refresh(_characterService.InStorageClasses);
        }

        private void ValidateAndEmit() => _onValidate?.Invoke(_characterService.ValidateCurrentClasses());

        // ------------------------------------------------------
        // Internal class: Displayers generic manager with pool
        // ------------------------------------------------------
        private class PooledDisplayerGroup<T> where T : ClassDisplayerUI
        {
            private readonly PrefabPool<T> _pool;
            private readonly List<T> _displayers;
            private readonly System.Action<ClassDisplayerUI> _action;

            public PooledDisplayerGroup(T prefab, Transform parent, System.Action<ClassDisplayerUI> action)
            {
                _pool = new PrefabPool<T>(prefab, parent);
                _displayers = new List<T>();
                _action = action;
            }

            /// <summary> Initializes the Displayers list from the class array. </summary>
            public void Initialize(ClassSO[] classes)
            {
                _pool.ReleaseAll();
                _displayers.Clear();

                foreach (var cls in classes)
                {
                    var displayer = _pool.Get();
                    displayer.SetClass(cls);
                    displayer.SetAction(_action);
                    _displayers.Add(displayer);
                }
            }

            /// <summary>
            /// Update existing or create di -plays as necessary.
            /// Release the excess of Displayers to the pool.
            /// </summary>
            public void Refresh(ClassSO[] classes)
            {
                // 1. Release leftover
                for (int i = classes.Length; i < _displayers.Count; i++)
                {
                    _pool.Release(_displayers[i]);
                }

                // 2. Cut list if they are left over
                if (_displayers.Count > classes.Length)
                {
                    _displayers.RemoveRange(classes.Length, _displayers.Count - classes.Length);
                }

                // 3. Update or create necessary dysplayers
                for (int i = 0; i < classes.Length; i++)
                {
                    if (i < _displayers.Count)
                    {
                        _displayers[i].SetClass(classes[i]);
                    }
                    else
                    {
                        var displayer = _pool.Get();
                        displayer.SetClass(classes[i]);
                        displayer.SetAction(_action);
                        _displayers.Add(displayer);
                    }
                }
            }
        }
    }
}
